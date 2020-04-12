using NetFwTypeLib;

namespace FirewallWidget.Manager.DTO
{
    public class FirewallRuleDto
    {
        public ProfileDto Profile { get; internal set; }

        public string ProgramPath { get; internal set; }

        public string Name { get; internal set; }

        public RuleDirectionDto Direction { get; set; }

        internal INetFwRule3 FwRule { get; set; }
    }
}
