using FirewallWidget.ChildForms;
using FirewallWidget.Manager.DTO;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
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

        private void HideForm(object sender, EventArgs e)
        {
            HideForm();
        }

        private void MainForm_Shown(object sender, System.EventArgs e)
        {
            Size = new Size(42, Screen.PrimaryScreen.WorkingArea.Height);
            Location = new Point(-40, 0);
            CalcRulesMaxScroll();
        }

        private void OptionsMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            HideForm();
        }

        private void SetIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetPBoxFromSender(sender) is PictureBox pbox)
            {
                var ofd = new OpenFileDialog()
                {
                    AddExtension = true,
                    CheckFileExists = true,
                    CheckPathExists = true,
                    Filter = "Icon File (*.ico)|*.ico|Executable (*.exe)|*.exe",
                    Multiselect = false,
                    Title = "Select file to get icon"
                };

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (pbox.Tag is RuleDto ruleDto)
                    {
                        ruleDto.Icon = Path.GetExtension(ofd.FileName) == ".exe"
                            ? GetExeIcon(ofd.FileName)
                            : new Icon(ofd.FileName).ToBitmap();

                        ruleService.Update(ruleDto);
                        LoadRules();
                    }

                }
            }
            HideForm();
        }

        private void PboxContext_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (sender is ContextMenuStrip menu &&
                menu.SourceControl is PictureBox pbox &&
                pbox.Tag is RuleDto ruleDto)
            {
                removeIconToolStripMenuItem.Enabled = ruleDto.Icon != null;
            }
        }

        private void RemoveRuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetPBoxFromSender(sender) is PictureBox pbox &&
                pbox.Tag is RuleDto ruleDto)
            {
                ruleService.Delete(ruleDto.Id);
                LoadRules();
            }

            HideForm();
        }

        private void RemoveIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetPBoxFromSender(sender) is PictureBox pbox &&
                pbox.Tag is RuleDto ruleDto)
            {
                ruleDto.Icon = null;
                ruleService.Update(ruleDto);
                LoadRules();
            }

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

        private void ScrollRulesUp(object sender, EventArgs e)
        {
            if (!canScrollRulesUp)
            { return; }

            var p = scrollRulesPosition;
            scrollRulesPosition = Math.Max(0, scrollRulesPosition - 37);

            if (p != scrollRulesPosition)
            {
                if (scrollRulesPosition <= 0)
                {
                    pnlRules.AutoScrollPosition = new Point(0, 0);
                    canScrollRulesUp = false;
                    pnlScrollUp.Disable();
                }
                else
                { pnlRules.VerticalScroll.Value = scrollRulesPosition; }

                canScrollRulesDown = true;
                pnlScrollDown.Enable();
            }
        }

        private void ScrollRulesDown(object sender, EventArgs e)
        {
            if (!canScrollRulesDown)
            { return; }

            var p = scrollRulesPosition;
            scrollRulesPosition = Math.Min(
                pnlRules.VerticalScroll.Maximum, scrollRulesPosition + 37);

            if (p != scrollRulesPosition)
            {
                if (scrollRulesPosition >= pnlRules.VerticalScroll.Maximum)
                {
                    pnlRules.AutoScrollPosition = new Point(0, pnlRules.VerticalScroll.Maximum);
                    canScrollRulesDown = false;
                    pnlScrollDown.Disable();
                }
                else
                { pnlRules.VerticalScroll.Value = scrollRulesPosition; }

                pnlScrollUp.Enable();
                canScrollRulesUp = true;
            }
        }
    }
}
