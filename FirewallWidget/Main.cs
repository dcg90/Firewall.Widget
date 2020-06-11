﻿using FirewallWidget.ChildForms;
using FirewallWidget.Manager.Contracts.Services;
using FirewallWidget.Manager.DTO;
using FirewallWidget.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FirewallWidget.Presentation
{
    public partial class MainForm : Form
    {
        private readonly IFirewallService firewallService;
        private readonly IRuleService ruleService;
        private int scrollRulesPosition = 0;
        private bool canScrollRulesDown, canScrollRulesUp;

        public MainForm(IFirewallService firewallService, IRuleService ruleService)
        {
            this.firewallService = firewallService;
            this.ruleService = ruleService;


            InitializeComponent();
            LoadRules();

            SetOutBoundConnectionState(publicAllowOutboundToolStripMenuItem, ProfileDto.Public);
            pnlRules.AutoScrollPosition = new Point(0, 0);
            pnlScrollUp.Disable();
        }

        private void SetOutBoundConnectionState(ToolStripMenuItem item, ProfileDto profile)
        {
            item.CheckState =
                firewallService.OutboundConnectionsAllowedOn(profile)
                ? CheckState.Checked
                : CheckState.Unchecked;
        }

        private void LoadRules()
        {
            var lastY = 5;
            var removeRules = new List<RuleDto>();
            pnlRules.Controls.Clear();

            foreach (var rule in ruleService.ReadAll())
            {
                ProcessRule(rule, removeRules, ref lastY);
            }

            foreach (var ruleToDelete in removeRules)
            { ruleService.Delete(ruleToDelete.Id); }

            ResetRulesScroll();
        }

        private void ProcessRule(RuleDto rule, List<RuleDto> removeRules, ref int lastY)
        {
            var matchingFwRules = firewallService.GetMatchingRules(rule.Name, rule.Profile, rule.Direction);
            var matchingFwRulesCount = matchingFwRules.Count();
            if (matchingFwRulesCount == 0)
            { DeleteRule(rule, removeRules); }
            else
            {
                FirewallRuleDto fwRule = null;
                if (matchingFwRulesCount > 1)
                {
                    using (var selectRule = new SelectRule(rule, matchingFwRules))
                    {
                        if (selectRule.ShowDialog() == DialogResult.OK)
                        { fwRule = selectRule.SelectedRule; }
                    }
                }
                else
                { fwRule = matchingFwRules.Single(); }

                if (fwRule == null)
                { DeleteRule(rule, removeRules); }
                else
                {
                    var pbox = BuildPictureBox(lastY, rule, fwRule);
                    lastY += pbox.Height + 5;
                }
            }
        }

        private static void DeleteRule(RuleDto rule, List<RuleDto> removeRules)
        {
            if (MessageBox.Show(
                "Rule '" + rule.Name + "' could not be imported.\nDelete this rule?", "Error",
                MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            { removeRules.Add(rule); }
        }

        private PictureBox BuildPictureBox(int lastY, RuleDto rule, FirewallRuleDto fwRule)
        {
            var (icon, iconGrayScale) = LoadRuleIcon(rule, fwRule);


            var pbox = new PictureBox
            {
                Location = new Point(5, lastY),
                Image = firewallService.IsEnabled(fwRule)
                    ? icon
                    : iconGrayScale,
                Size = new Size(32, 32),
                Cursor = Cursors.Hand,
                Tag = rule
            };
            ruleNameToolTip.SetToolTip(pbox, fwRule.Name);
            pbox.Click += (sender, e) =>
            {
                try
                {
                    var enabled = firewallService.SwitchEnabled(fwRule);
                    pbox.Image = enabled ? icon : iconGrayScale;
                }
                catch (Exception exc)
                {
                    if (MessageBox.Show(
                        "The following error occurred: \n" + exc.Message + "\nDelete this rule?", "Error",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                        ruleService.Delete(rule.Id);
                        LoadRules();
                    }
                }
            };
            pbox.ContextMenuStrip = pboxContext;
            pbox.MouseLeave += HideForm;
            pnlRules.Controls.Add(pbox);
            return pbox;
        }

        private (Bitmap, Bitmap) LoadRuleIcon(RuleDto rule, FirewallRuleDto fwRule)
        {
            Bitmap iconGrayScale;
            Bitmap icon;

            if (rule.Icon != null)
            { (icon, iconGrayScale) = (rule.Icon, (Bitmap)ToolStripRenderer.CreateDisabledImage(rule.Icon)); }
            else if (File.Exists(fwRule.ProgramPath))
            {
                icon = GetExeIcon(fwRule.ProgramPath);
                iconGrayScale = (Bitmap)ToolStripRenderer.CreateDisabledImage(icon);
            }
            else
            {
                icon = CreateDefaultRuleIcon(true);
                iconGrayScale = CreateDefaultRuleIcon(false);
            }

            DrawDirection(fwRule.Direction, icon);
            DrawDirection(fwRule.Direction, iconGrayScale);

            return (icon, iconGrayScale);
        }

        private static Bitmap GetExeIcon(string filename)
        {
            return Icon.ExtractAssociatedIcon(filename).ToBitmap();
        }

        private static Bitmap CreateDefaultRuleIcon(bool enabledRule)
        {
            var icon = new Bitmap(32, 32);
            var graphics = Graphics.FromImage(icon);
            var rect = new RectangleF(0, 0, 32f, 32f);
            graphics.FillEllipse(enabledRule ? Brushes.DarkCyan : Brushes.Gray, rect);
            graphics.DrawLine(new Pen(Color.Red, 1.5f), new Point(), new Point(32, 32));
            graphics.DrawLine(new Pen(Color.Red, 1.5f), new Point(32, 0), new Point(0, 32));

            return icon;
        }

        private void DrawDirection(RuleDirectionDto direction, Bitmap icon)
        {
            var text = direction.ToString();
            var font = new Font("Consolas", 9.5f, FontStyle.Regular, GraphicsUnit.Point);
            var graphics = Graphics.FromImage(icon);
            var textSize = graphics.MeasureString(text, font);

            var rectangle = new RectangleF(
                new PointF(icon.Width - textSize.Width + 2, icon.Height - textSize.Height + 2),
                new SizeF(textSize.Width - 2, textSize.Height - 2));

            var brush = new SolidBrush(Color.FromArgb(170, Color.Black));

            graphics.FillPath(brush, RoundedRect(rectangle, 2));
            graphics.DrawString(text, font, Brushes.White, rectangle.Location);
        }

        // Reference: https://stackoverflow.com/a/33853557/4152153
        public static GraphicsPath RoundedRect(RectangleF bounds, float radius)
        {
            var diameter = radius * 2;
            var size = new SizeF(diameter, diameter);
            var arc = new RectangleF(bounds.Location, size);
            var path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc, 180, 90);

            // top right arc  
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        private void ShowForm()
        {
            Location = new Point(0, 0);
        }

        private void HideForm()
        {
            if (!ClientRectangle.Contains(PointToClient(Cursor.Position)))
            { Location = new Point(-40, 0); }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var @params = base.CreateParams;
                @params.ExStyle |= 0x80;
                return @params;
            }
        }

        private static PictureBox GetPBoxFromSender(object sender)
        { return ((sender as ToolStripItem)?.Owner as ContextMenuStrip)?.SourceControl as PictureBox; }

        private void ResetRulesScroll()
        {
            pnlRules.VerticalScroll.Maximum = Math.Max(0, pnlRules.Controls.Count > 0
                 ? pnlRules.Controls.OfType<Control>().Max(c => c.Location.Y + c.Height + 5) - pnlRules.Height
                 : 0);
            if (pnlRules.VerticalScroll.Maximum > 0)
            { pnlRules.VerticalScroll.Maximum = Math.Max(pnlRules.VerticalScroll.Maximum, 38); }

            pnlRules.AutoScrollPosition = new Point(0, 0);
            scrollRulesPosition = 0;
            pnlScrollUp.Disable();
            canScrollRulesUp = false;

            canScrollRulesDown = pnlRules.VerticalScroll.Maximum > 0;
            if (canScrollRulesDown)
            { pnlScrollDown.Enable(); }
            else
            { pnlScrollDown.Disable(); }
        }
    }
}
