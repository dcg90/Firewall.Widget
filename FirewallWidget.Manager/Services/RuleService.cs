using AutoMapper;

using FirewallWidget.Data;
using FirewallWidget.DataAccess.Contracts.Repositories;
using FirewallWidget.Manager.Contracts.Services;
using FirewallWidget.Manager.DTO;
using FirewallWidget.Manager.Extensions;
using FirewallWidget.Manager.Validators;

using System.Collections.Generic;
using System.Linq;

namespace FirewallWidget.Manager.Services
{
    public class RuleService : IRuleService
    {
        private readonly IRulesRepository rulesRepository;
        private readonly IMapper mapper;
        private static readonly RuleValidator validator = new RuleValidator();

        public RuleService(
            IRulesRepository rulesRepository,
            IMapper mapper)
        {
            this.rulesRepository = rulesRepository;
            this.mapper = mapper;
        }

        public ServiceResult<RuleDto> Create(RuleDto ruleDto)
        {
            var v = validator.Validate(ruleDto);
            if (!v.IsValid)
            { return ServiceResult<RuleDto>.BadInput(v.ExtractErrors()); }

            var rule = rulesRepository.Create(mapper.Map<Rule>(ruleDto));
            return ServiceResult<RuleDto>.Success(mapper.Map<RuleDto>(rule));
        }

        public ServiceResult<int> Delete(int key)
        {
            var rule = rulesRepository.Read(key);
            if (rule == null)
            { return ServiceResult<int>.NotFound(); }

            return ServiceResult<int>.Success(rulesRepository.Delete(key));
        }

        public IEnumerable<RuleDto> ReadAll()
        {
            return mapper.Map<IEnumerable<RuleDto>>(
                rulesRepository.Read(r => true).OrderBy(r => r.Name));
        }

        public ServiceResult<RuleDto> Read(int key)
        {
            var rule = rulesRepository.Read(key);
            if (rule == null)
            { return ServiceResult<RuleDto>.NotFound(); }

            return ServiceResult<RuleDto>.Success(mapper.Map<RuleDto>(rule));
        }

        public ServiceResult<RuleDto> Update(RuleDto ruleDto)
        {
            var v = validator.Validate(ruleDto);
            if (!v.IsValid)
            { return ServiceResult<RuleDto>.BadInput(v.ExtractErrors()); }

            var rule = rulesRepository.Read(ruleDto.Id);
            if (rule == null)
            { return ServiceResult<RuleDto>.NotFound(); }

            rule = rulesRepository.Update(mapper.Map<Rule>(ruleDto));

            return ServiceResult<RuleDto>.Success(mapper.Map<RuleDto>(rule));
        }

        public ServiceResult<int> Create(params RuleDto[] rules)
        {
            rules = rules ?? new RuleDto[0];
            var inserted = 0;
            foreach (var rule in rules)
            {
                if (!rulesRepository.RuleExist(rule.Name))
                {
                    rulesRepository.Create(mapper.Map<Rule>(rule));
                    inserted++;
                }
            }

            return ServiceResult<int>.Success(inserted);
        }
    }
}
