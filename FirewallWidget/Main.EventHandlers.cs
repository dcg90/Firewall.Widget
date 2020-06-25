using FirewallWidget.ChildForms;
using FirewallWidget.Manager.DTO;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Drawing;
using System.Windows.Forms;

using static FirewallWidget.Presentation.FirewallWidgetConstants;

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
                LoadOptions();
                HideForm();
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
                    var ar = ruleService.Create(addRulesForm.SelectedRules.ToArray());
                    if (ar.Successful)
                    { AddRules(ar.DTO); }
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
            Size = new Size(FORM_WIDTH, Screen.PrimaryScreen.WorkingArea.Height);
            HideForm();
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

        private void PnlRules_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(RULE_CONTROL_DRAG_FORMAT)
                ? DragDropEffects.Move
                : DragDropEffects.None;
        }

        private void PnlRules_DragDrop(object sender, DragEventArgs e)
        {
            pnlRules.Invalidate();
            if (e.Data.GetDataPresent(RULE_CONTROL_DRAG_FORMAT))
            {
                if (e.Data.GetData(RULE_CONTROL_DRAG_FORMAT) is RuleControl draggingRuleControl)
                {
                    var cursorLocation = new Point(e.X, e.Y);
                    var (ruleControl, cursorBelowMiddle) = GetRuleDragInfoFromCursorLocation(cursorLocation);
                    RuleControl prev, next;

                    if (ruleControl != null)
                    {
                        prev = cursorBelowMiddle ? ruleControl.Previous : ruleControl;
                        next = cursorBelowMiddle ? ruleControl : ruleControl.Next;
                        if (prev == draggingRuleControl || next == draggingRuleControl)
                        { return; }
                    }
                    else
                    {
                        prev = FindRuleControlFromPoint(draggingRuleControl, e.Y, -1);
                        next = FindRuleControlFromPoint(draggingRuleControl, e.Y, 1);
                    }

                    DetachRuleControl(draggingRuleControl);
                    AttachRuleControl(draggingRuleControl, prev, next);
                    UpdateRulesOrder();
                }
            }
        }

        private void PnlRules_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(RULE_CONTROL_DRAG_FORMAT))
            {
                var cursorLocation = new Point(e.X, e.Y);
                var (ruleControl, cursorBelowMiddle) = GetRuleDragInfoFromCursorLocation(cursorLocation);
                if (ruleControl != null)
                {
                    var drawRectangle = cursorBelowMiddle
                          ? new RectangleF(
                                2, ruleControl.Location.Y - (VERTICAL_RULES_MARGIN / 2.0f) - 1,
                                pnlRules.Width - 4, 2)
                           : new RectangleF(
                               2, ruleControl.Location.Y + ruleControl.Height + (VERTICAL_RULES_MARGIN / 2.0f) - 1,
                               pnlRules.Width - 4, 2);

                    var gr = pnlRules.CreateGraphics();
                    gr.FillRectangle(Brushes.Gray, drawRectangle);

                    var invalidateRegion = new Region(pnlRules.ClientRectangle);
                    invalidateRegion.Exclude(drawRectangle);
                    pnlRules.Invalidate(invalidateRegion);
                }
            }
        }

        private void PnlRules_DragLeave(object sender, EventArgs e)
        {
            pnlRules.Invalidate();
        }

        private void PnlScroll_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(RULE_CONTROL_DRAG_FORMAT))
            {
                if (sender is Panel scroll)
                {
                    switch (scroll.Tag)
                    {
                        case SCROLL_UP_TAG:
                            ScrollRulesUp();
                            break;
                        case SCROLL_DOWN_TAG:
                            ScrollRulesDown();
                            break;
                    }
                }
            }
        }

        private void PnlScroll_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
            if (sender is Panel scroll && scroll.Tag is string tag)
            {
                var canDrag = tag == SCROLL_UP_TAG
                    ? canScrollRulesUp
                    : tag == SCROLL_DOWN_TAG
                        ? canScrollRulesDown
                        : false;
                if (e.Data.GetDataPresent(RULE_CONTROL_DRAG_FORMAT) && canDrag)
                { e.Effect = DragDropEffects.Move; }
            }
        }

    }
}
