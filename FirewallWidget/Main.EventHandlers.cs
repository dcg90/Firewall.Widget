using FirewallWidget.ChildForms;
using FirewallWidget.Manager.DTO;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FirewallWidget.Presentation
{
    public partial class MainForm
    {
        private void BtnOptions_Click(object sender, System.EventArgs e)
        {
            optionsMenu.Show(Cursor.Position, ToolStripDropDownDirection.BelowRight);
        }

        private void ExitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm();
            using (var options = Provider.GetRequiredService<OptionsForm>())
            {
                options.ShowDialog();
            }
            HideForm();
        }

        private void AddRulesToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowForm();
            using (var addRulesForm = Provider.GetRequiredService<AddRulesForm>())
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

        private void HideForm(object sender, EventArgs e)
        {
            HideForm();
        }

        private void MainForm_Shown(object sender, System.EventArgs e)
        {
            Size = new Size(42, Screen.PrimaryScreen.WorkingArea.Height);
            Location = new Point(-40, 0);
            ResetRulesScroll();
        }

        private void OptionsMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            HideForm();
        }

        private void PboxContext_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            HideForm();
        }

        private void PublicAllowOutboundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            firewallService.SwitchOutboundConnectionsStateOn(ProfileDto.Public);
            SetOutBoundConnectionState(publicAllowOutboundToolStripMenuItem, ProfileDto.Public);
        }

        private void ScrollRulesUpHandler(object sender, EventArgs e)
        {
            ScrollRulesUp();
        }

        private void ScrollRulesDownHandler(object sender, EventArgs e)
        {
            ScrollRulesDown();
        }
    }
}
