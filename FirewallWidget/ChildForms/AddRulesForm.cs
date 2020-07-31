using FirewallWidget.Manager.Contracts.Services;
using FirewallWidget.Manager.DTO;
using FirewallWidget.Presentation;
using FirewallWidget.Presentation.ChildForms.CreateRule;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using static FirewallWidget.Presentation.FirewallWidgetConstants;

namespace FirewallWidget.ChildForms
{
    public partial class AddRulesForm : NextToMainForm
    {
        private readonly IFirewallService firewallService;
        private readonly IRuleService ruleService;
        private readonly IOptionsService optionsService;
        private ProfileItem currentProfile;
        private DirectionItem currentDirection;

        internal List<RuleDto> SelectedRules { get; private set; }

        public AddRulesForm(MainForm main, IFirewallService firewallService,
            IRuleService ruleService, IOptionsService optionsService)
            : base(main, optionsService)
        {
            this.firewallService = firewallService;
            this.ruleService = ruleService;
            this.optionsService = optionsService;

            InitializeComponent();

            FillCombos();
            LoadRules();
        }

        private void FillCombos()
        {
            cboxProfiles.Items.AddRange(PROFILES);
            currentProfile = PROFILES[2];

            cboxDirections.Items.AddRange(DIRECTIONS);
            currentDirection = DIRECTIONS[1];

            cboxProfiles.SelectedItem = currentProfile;
            cboxDirections.SelectedItem = currentDirection;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Cbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboxProfiles.SelectedItem is ProfileItem profile &&
                cboxDirections.SelectedItem is DirectionItem direction &&
                (currentProfile != profile ||
                currentDirection != direction))
            {
                currentProfile = profile;
                currentDirection = direction;

                LoadRules();
            }
        }

        private void LoadRules()
        {
            lboxRules.Items.Clear();
            var ruleNames = new HashSet<string>(ruleService
                .ReadRules(currentProfile.Profile, currentDirection.Direction)
                .Select(r => r.Name));

            foreach (var rule in firewallService.GetRules(currentProfile.Profile, currentDirection.Direction))
            {
                if (!ruleNames.Contains(rule.Name))
                { lboxRules.Items.Add(new RuleItem { Rule = rule }); }
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (lboxRules.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "No rules selected.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.None;
            }
            else
            {
                SelectedRules = new List<RuleDto>();
                foreach (RuleItem item in lboxRules.SelectedItems)
                {
                    SelectedRules.Add(new RuleDto
                    {
                        Name = item.Rule.Name,
                        Profile = item.Rule.Profile,
                        ProgramPath = item.Rule.ProgramPath,
                        Direction = item.Rule.Direction
                    });
                }
            }

        }

        private void BtnMenu_Click(object sender, EventArgs e)
        {
            addRuleMenu.Show(PointToScreen(
                    new Point(
                        btnMenu.Location.X + 2,
                        btnMenu.Location.Y + btnMenu.Height + 2)));
        }

        private void RefreshListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            firewallService.Refresh();
            LoadRules();
        }

        private void CreateRuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var createRule = new CreateFirewallRuleForm(this, optionsService, firewallService))
            {
                if (createRule.ShowDialog() == DialogResult.OK && createRule.Rule != null)
                {
                    var rule = new RuleItem { Rule = createRule.Rule };
                    lboxRules.Items.Add(rule);
                    lboxRules.SelectedItems.Clear();
                    lboxRules.SelectedItem = rule;
                }
            }
        }
    }
}
