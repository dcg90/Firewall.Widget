using FirewallWidget.Manager.DTO;

using System.Collections.Generic;
using System.Windows.Forms;

namespace FirewallWidget.ChildForms
{
    public partial class SelectRule : Form
    {
        public SelectRule(RuleDto rule, IEnumerable<FirewallRuleDto> rules)
        {
            InitializeComponent();

            lblHeader.Text = string.Format(lblHeader.Text, rule.Profile, rule.Direction, rule.Name);

            foreach (var r in rules)
            {
                lboxChoices.Items.Add(new RuleItem
                { Rule = r });
            }

            lboxChoices.SelectedIndex = 0;
        }

        public FirewallRuleDto SelectedRule => ((RuleItem)lboxChoices.SelectedItem).Rule;
    }
}
