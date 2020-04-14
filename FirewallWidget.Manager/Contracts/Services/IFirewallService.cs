using FirewallWidget.Manager.DTO;

using System.Collections.Generic;

namespace FirewallWidget.Manager.Contracts.Services
{
    public interface IFirewallService
    {
        IEnumerable<FirewallRuleDto> GetRules(ProfileDto profile, RuleDirectionDto direction);

        bool IsEnabled(FirewallRuleDto rule);

        IEnumerable<FirewallRuleDto> GetMatchingRules(string name, ProfileDto profile, RuleDirectionDto direction);

        bool SwitchEnabled(FirewallRuleDto rule);

        void Refresh();
    }
}
