using FirewallWidget.Manager.DTO;

using System.Collections.Generic;

namespace FirewallWidget.Manager.Contracts.Services
{
    public interface IRuleService
    {
        ServiceResult<RuleDto> Create(RuleDto rule);

        ServiceResult<int> Create(params RuleDto[] rules);

        ServiceResult<RuleDto> Read(int key);

        ServiceResult<int> Delete(int key);

        ServiceResult<RuleDto> Update(RuleDto ruleDto);

        IEnumerable<RuleDto> ReadAll();

        IEnumerable<RuleDto> ReadRules(ProfileDto profile, RuleDirectionDto direction);
    }
}
