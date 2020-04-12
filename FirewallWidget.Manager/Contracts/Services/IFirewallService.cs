using FirewallWidget.Manager.DTO;

using System.Collections.Generic;

namespace FirewallWidget.Manager.Contracts.Services
{
    public interface IFirewallService
    {
        IEnumerable<FirewallRuleDto> GetRules(ProfileDto profile, RuleDirectionDto direction);

        bool IsEnabled(string name, ProfileDto profile, RuleDirectionDto direction);

        bool Exists(string name, ProfileDto profile, RuleDirectionDto direction);

        bool SwitchEnabled(string name, ProfileDto profile, RuleDirectionDto direction);
    }
}
