using FirewallWidget.Manager.DTO;

using System.Collections.Generic;

namespace FirewallWidget.Manager.Contracts.Services
{
    public interface IFirewallService
    {
        FirewallRuleDto CreateFirewallRule(CreateFirewallRuleDto rule);

        IEnumerable<FirewallRuleDto> GetRules(ProfileDto profile, RuleDirectionDto direction);

        bool IsEnabled(FirewallRuleDto rule);

        IEnumerable<FirewallRuleDto> GetMatchingRules(string name, ProfileDto profile, RuleDirectionDto direction);

        bool SwitchEnabled(FirewallRuleDto rule);

        bool OutboundConnectionsAllowedOn(ProfileDto profileDto);

        void SwitchOutboundConnectionsStateOn(ProfileDto profileDto);

        void Refresh();
    }
}
