using AutoMapper;

using FirewallWidget.Data;
using FirewallWidget.Manager.DTO;

namespace FirewallWidget.Manager.MappingProfiles
{
    internal class RuleProfile : Profile
    {
        public RuleProfile()
        {
            CreateMap<RuleDto, Rule>()
                .ReverseMap();
        }
    }
}
