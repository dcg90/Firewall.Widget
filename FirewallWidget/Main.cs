using FirewallWidget.ChildForms;
using FirewallWidget.Manager.Contracts.Services;
using FirewallWidget.Manager.DTO;

using System;
using System.Collections.Generic;
using System.Drawing;
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

            var pbox = new PictureBox
            {
                Location = new Point(5, lastY),
                Image = firewallService.IsEnabled(rule.Name, rule.Profile, rule.Direction)
                    ? icon
                    : iconGrayScale,
                Size = icon.Size,
                Cursor = Cursors.Hand,
            };
            var toolTip = new ToolTip();
            toolTip.SetToolTip(pbox, rule.Name);
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
            using (var addRulesForm = new AddRulesForm(this, firewallService))
            {
                if (addRulesForm.ShowDialog() == DialogResult.OK)
                {
                    ruleService.Create(addRulesForm.SelectedRules.ToArray());
                    LoadRules();
                }
            }
            HideForm(this, EventArgs.Empty);
        }

        private void ShowForm(object sender, EventArgs e)
        { Location = new Point(0, 0); }

        private void HideForm(object sender, EventArgs e)
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
            HideForm(sender, e);
        }
    }
}
