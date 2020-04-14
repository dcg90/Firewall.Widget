using FirewallWidget.Manager.DTO;

namespace FirewallWidget.ChildForms
{
    internal class RuleItem
    {
        public FirewallRuleDto Rule { get; set; }

        public override string ToString()
        {
            return (Rule?.Name ?? "<empty>") + " (" + (Rule?.ProgramPath ?? "<empty>") + ")";
        }
    }
}
