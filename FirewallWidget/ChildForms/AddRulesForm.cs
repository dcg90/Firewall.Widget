using FirewallWidget.Manager.Contracts.Services;
using FirewallWidget.Manager.DTO;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FirewallWidget.ChildForms
{
    public partial class AddRulesForm : Form
    {
        private readonly IFirewallService firewallService;
        private ProfileItem currentProfile;
        private DirectionItem currentDirection;

        internal List<RuleDto> SelectedRules { get; private set; }

        public AddRulesForm(Form main, IFirewallService firewallService)
        {
            this.firewallService = firewallService;

            InitializeComponent();

            Location = PointToScreen(new Point(main.Location.X + main.Width + 2, main.Location.Y));

            FillCombos();
            LoadRules();
        }

        private void FillCombos()
        {
            cboxProfiles.Items.Add(new ProfileItem { Display = "Domain", Profile = ProfileDto.Domain });
            cboxProfiles.Items.Add(new ProfileItem { Display = "Private", Profile = ProfileDto.Private });
            cboxProfiles.Items.Add(currentProfile = new ProfileItem { Display = "Public", Profile = ProfileDto.Public });

            cboxDirections.Items.Add(new DirectionItem { Display = "In", Direction = RuleDirectionDto.In });
            cboxDirections.Items.Add(currentDirection = new DirectionItem { Display = "Out", Direction = RuleDirectionDto.Out });
            cboxDirections.Items.Add(new DirectionItem { Display = "Max", Direction = RuleDirectionDto.Max });

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

            foreach (var rule in firewallService.GetRules(currentProfile.Profile, currentDirection.Direction))
            { lboxRules.Items.Add(new RuleItem { Rule = rule }); }
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

        private void BtnRefreshRules_Click(object sender, EventArgs e)
        {
            firewallService.Refresh();
            LoadRules();
        }
    }

    internal class RuleItem
    {
        public FirewallRuleDto Rule { get; set; }

        public override string ToString()
        {
            return (Rule?.Name ?? "<empty>") + " (" + (Rule?.ProgramPath ?? "<empty>") + ")";
        }
    }

    internal class ProfileItem
    {
        public string Display { get; set; }

        public ProfileDto Profile { get; set; }

        public override string ToString()
        {
            return Display;
        }
    }

    internal class DirectionItem
    {
        public string Display { get; set; }

        public RuleDirectionDto Direction { get; set; }

        public override string ToString()
        {
            return Display;
        }
    }
}
