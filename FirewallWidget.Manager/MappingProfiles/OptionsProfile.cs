using AutoMapper;

using FirewallWidget.Data;
using FirewallWidget.Manager.DTO;

namespace FirewallWidget.Manager.MappingProfiles
{
    internal class OptionsProfile : Profile
    {
        public OptionsProfile()
        {
            CreateMap<Options, OptionsDto>()
                .ReverseMap()
                    .ForMember(o => o.Id, o => o.Ignore());
        }
    }
}
