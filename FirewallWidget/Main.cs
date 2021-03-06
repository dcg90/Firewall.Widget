﻿using FirewallWidget.ChildForms;
using FirewallWidget.Manager.Contracts.Services;
using FirewallWidget.Manager.DTO;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using static FirewallWidget.Presentation.FirewallWidgetConstants;

namespace FirewallWidget.Presentation
{
    public partial class MainForm : NoTaskBarAltTabForm
    {
        public IServiceProvider Provider { get; }

        private readonly IFirewallService firewallService;
        private readonly IRuleService ruleService;
        private readonly IOptionsService optionsService;
        private int scrollRulesPosition = 0;
        private bool canScrollRulesDown, canScrollRulesUp;

        private OptionsDto options;

        public MainForm(IServiceProvider provider)
        {
            Provider = provider;
            firewallService = Provider.GetRequiredService<IFirewallService>(); ;
            ruleService = Provider.GetRequiredService<IRuleService>();
            optionsService = Provider.GetRequiredService<IOptionsService>();
            LoadOptions();

            InitializeComponent();
            MyInitializeComponent();

            LoadRules();

            SetOutBoundConnectionState(publicAllowOutboundToolStripMenuItem, ProfileDto.Public);
            pnlRules.AutoScrollPosition = new Point(0, 0);
            pnlScrollUp.Disable();
        }

        private void LoadOptions()
        {
            options = optionsService.ReadOptions();
        }

        private void MyInitializeComponent()
        {
            pnlRules.VerticalScroll.Visible = false;
            pnlRules.MouseWheel += (sender, e) =>
            {
                if (e.Delta < 0)
                { ScrollRulesDown(); }
                else if (e.Delta > 0)
                { ScrollRulesUp(); }
            };
            pnlScrollDown.Tag = SCROLL_DOWN_TAG;
            pnlScrollUp.Tag = SCROLL_UP_TAG;
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
            pnlRules.Controls.Clear();
            AddRulesToPanel(ruleService.ReadAll());
        }

        private RuleControl AddRulesToPanel(IEnumerable<RuleDto> rules)
        {
            var removeRules = new List<RuleDto>();
            RuleControl prev = null;
            RuleControl head = null;

            foreach (var rule in rules)
            {
                var ruleControl = ProcessRule(rule, removeRules, prev);
                if (ruleControl != null)
                {
                    head = head ?? ruleControl;
                    ruleControl.HideForm += (rc) => { HideForm(); };
                    ruleControl.DeleteRule += (rc, r) =>
                    {
                        var deleteResult = ruleService.Delete(r.Id);
                        if (deleteResult.Successful)
                        {
                            DetachRuleControl(rc);
                            pnlRules.Controls.Remove(rc);
                        }
                    };
                    ruleControl.UpdateRule += (rc, r) => { ruleService.Update(r); };
                    pnlRules.Controls.Add(ruleControl);

                    if (prev != null)
                    { prev.Next = ruleControl; }

                    prev = ruleControl;
                }
            }

            foreach (var ruleToDelete in removeRules)
            { ruleService.Delete(ruleToDelete.Id); }

            ResetRulesScroll();

            return head;
        }

        private void DetachRuleControl(RuleControl rc)
        {
            if (rc.Next != null)
            { rc.Next.SetPrevious(rc.Previous); }
            if (rc.Previous != null)
            { rc.Previous.Next = rc.Next; }
        }

        private void AttachRuleControl(RuleControl rc, RuleControl prev, RuleControl next)
        {
            if (prev != null)
            { prev.Next = rc; }
            if (next != null)
            { next.SetPrevious(rc); }

            rc.SetPrevious(prev);
            rc.Next = next;
        }

        private RuleControl ProcessRule(RuleDto rule, List<RuleDto> removeRules, RuleControl prev)
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
                    using (var selectRule = new SelectRuleForm(rule, matchingFwRules))
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
                    var ruleControl = prev == null
                        ? new RuleControl(rule, fwRule, FIRST_RULE_LOCATION, firewallService)
                        : new RuleControl(rule, fwRule, prev, firewallService);
                    return ruleControl;
                }
            }

            return null;
        }

        private static void DeleteRule(RuleDto rule, List<RuleDto> removeRules)
        {
            if (MessageBox.Show(
                "Rule '" + rule.Name + "' could not be imported.\nDelete this rule?", "Error",
                MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            { removeRules.Add(rule); }
        }

        private void ShowForm()
        {
            Location = options.DockLeft
                ? new Point(0, 0)
                : new Point(Screen.PrimaryScreen.WorkingArea.Width - FORM_WIDTH, 0);
        }

        private void HideForm()
        {
            if (!ClientRectangle.Contains(PointToClient(Cursor.Position)))
            {
                Location = options.DockLeft
                  ? new Point(-FORM_WIDTH + 2, 0)
                  : new Point(Screen.PrimaryScreen.WorkingArea.Width - 2, 0);
            }
        }

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

        private void ScrollRulesUp()
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

        private void ScrollRulesDown()
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

        private (RuleControl, bool) GetRuleDragInfoFromCursorLocation(Point cursorLocation)
        {
            var pnlCursorLocation = pnlRules.PointToClient(cursorLocation);
            var childControl = pnlRules.GetChildAtPoint(pnlCursorLocation);
            if (childControl is RuleControl ruleControl)
            {
                var ruleCursorLocation = ruleControl.PointToClient(cursorLocation);
                return (ruleControl, ruleCursorLocation.Y < ruleControl.Height / 2.0);
            }
            return (null, false);
        }

        private RuleControl FindRuleControlFromPoint(RuleControl dragging, int Y, int dirY)
        {
            var pt = pnlRules.PointToClient(new Point(pnlRules.Width / 2, Y));
            while (pnlRules.ClientRectangle.Contains(pt))
            {
                if (pnlRules.GetChildAtPoint(pt) is RuleControl ruleControl && ruleControl != dragging)
                { return ruleControl; }
                pt.Y += dirY * 31;
            }

            return null;
        }

        private void UpdateRulesOrder()
        {
            var head = Head;
            var order = 1;

            while (head != null)
            {
                head.Rule.Order = order++;
                ruleService.Update(head.Rule);
                head = head.Next;
            }
        }

        private RuleControl Head
            => pnlRules.Controls
                    .OfType<RuleControl>()
                    .FirstOrDefault(rc => rc.Previous == null);

        private RuleControl Tail
            => pnlRules.Controls
                .OfType<RuleControl>()
                .FirstOrDefault(rc => rc.Next == null);

        private void AddRules(IEnumerable<RuleDto> addedRules)
        {
            var prev = Tail;

            var head = AddRulesToPanel(addedRules);

            head.SetPrevious(prev);
            if (prev != null)
            { prev.Next = head; }

            UpdateRulesOrder();
        }
    }
}
