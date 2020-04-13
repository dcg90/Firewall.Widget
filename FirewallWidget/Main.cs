using FirewallWidget.ChildForms;
using FirewallWidget.Manager.Contracts.Services;
using FirewallWidget.Manager.DTO;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace FirewallWidget.Presentation
{
    public partial class MainForm : Form
    {
        private readonly IFirewallService firewallService;
        private readonly IRuleService ruleService;

        public MainForm(IFirewallService firewallService, IRuleService ruleService)
        {
            this.firewallService = firewallService;
            this.ruleService = ruleService;


            InitializeComponent();
            LoadRules();
        }

        private void LoadRules()
        {
            var lastY = 10;
            var removeRules = new List<RuleDto>();
            pnlRules.Controls.Clear();

            foreach (var rule in ruleService.ReadAll())
            {
                if (!firewallService.Exists(rule.Name, rule.Profile, rule.Direction) ||
                    !File.Exists(rule.ProgramPath))
                {
                    if (MessageBox.Show(
                         "Rule " + rule.Name + " or program path doesn't exists.\nDelete this rule?", "Error",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    { removeRules.Add(rule); }
                    continue;
                }

                var pbox = BuildPictureBox(lastY, rule);
                lastY += pbox.Height + 5;
            }

            foreach (var ruleToDelete in removeRules)
            { ruleService.Delete(ruleToDelete.Id); }
        }

        private PictureBox BuildPictureBox(int lastY, RuleDto rule)
        {
            var icon = Icon.ExtractAssociatedIcon(rule.ProgramPath).ToBitmap();
            var iconGrayScale = (Bitmap)ToolStripRenderer.CreateDisabledImage(icon);
            DrawDirection(rule, icon);
            DrawDirection(rule, iconGrayScale);


            var pbox = new PictureBox
            {
                Location = new Point(5, lastY),
                Image = firewallService.IsEnabled(rule.Name, rule.Profile, rule.Direction)
                    ? icon
                    : iconGrayScale,
                Size = icon.Size,
                Cursor = Cursors.Hand,
            };
            ruleNameToolTip.SetToolTip(pbox, rule.Name);
            pbox.Click += (sender, e) =>
            {
                try
                {
                    var enabled = firewallService.SwitchEnabled(rule.Name, rule.Profile, rule.Direction);
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
            pbox.MouseLeave += HideForm;
            pnlRules.Controls.Add(pbox);
            return pbox;
        }

        private void DrawDirection(RuleDto rule, Bitmap icon)
        {
            var text = rule.Direction.ToString();
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

        private void BtnOptions_Click(object sender, System.EventArgs e)
        {
            optionsMenu.Show(Cursor.Position, ToolStripDropDownDirection.BelowRight);
        }

        private void ExitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void AddRulesToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowForm();
            using (var addRulesForm = new AddRulesForm(this, firewallService))
            {
                if (addRulesForm.ShowDialog() == DialogResult.OK)
                {
                    ruleService.Create(addRulesForm.SelectedRules.ToArray());
                    LoadRules();
                }
            }
            HideForm();
        }

        private void ShowForm(object sender, EventArgs e)
        { ShowForm(); }

        private void ShowForm()
        {
            Location = new Point(0, 0);
        }

        private void HideForm(object sender, EventArgs e)
        {
            HideForm();
        }

        private void HideForm()
        {
            if (!ClientRectangle.Contains(PointToClient(Cursor.Position)))
            { Location = new Point(-40, 0); }
        }

        private void MainForm_Shown(object sender, System.EventArgs e)
        {
            Size = new Size(42, Screen.PrimaryScreen.WorkingArea.Height);
            Location = new Point(-40, 0);
        }

        private void OptionsMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            HideForm();
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
    }
}
