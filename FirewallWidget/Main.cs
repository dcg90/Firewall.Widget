using FirewallWidget.ChildForms;
using FirewallWidget.Manager.Contracts.Services;
using FirewallWidget.Manager.DTO;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FirewallWidget.Presentation
{
    public partial class MainForm : NoTaskBarAltTabForm
    {
        public IServiceProvider Provider { get; }

        private readonly IFirewallService firewallService;
        private readonly IRuleService ruleService;
        private int scrollRulesPosition = 0;
        private bool canScrollRulesDown, canScrollRulesUp;
        private readonly Point firstRuleLocation = new Point(5, 5);

        public MainForm(IServiceProvider provider)
        {
            Provider = provider;
            firewallService = Provider.GetRequiredService<IFirewallService>(); ;
            ruleService = Provider.GetRequiredService<IRuleService>(); ;

            InitializeComponent();
            MyInitializeComponent();

            LoadRules();

            SetOutBoundConnectionState(publicAllowOutboundToolStripMenuItem, ProfileDto.Public);
            pnlRules.AutoScrollPosition = new Point(0, 0);
            pnlScrollUp.Disable();
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
            var removeRules = new List<RuleDto>();
            pnlRules.Controls.Clear();
            RuleControl prev = null;

            foreach (var rule in ruleService.ReadAll())
            {
                var ruleControl = ProcessRule(rule, removeRules, prev);
                if (ruleControl != null)
                {
                    ruleControl.HideForm += (rc) => { HideForm(); };
                    ruleControl.DeleteRule += (rc, r) =>
                    {
                        var deleteResult = ruleService.Delete(r.Id);
                        if (deleteResult.Successful)
                        {
                            if (rc.Next != null)
                            { rc.Next.SetPrevious(rc.Previous, firstRuleLocation); }
                            if (rc.Previous != null)
                            { rc.Previous.Next = rc.Next; }

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
                        ? new RuleControl(rule, fwRule, firstRuleLocation, firewallService)
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
            Location = new Point(0, 0);
        }

        private void HideForm()
        {
            if (!ClientRectangle.Contains(PointToClient(Cursor.Position)))
            { Location = new Point(-40, 0); }
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
    }
}
