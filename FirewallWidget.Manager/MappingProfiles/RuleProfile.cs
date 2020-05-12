using AutoMapper;

using FirewallWidget.Data;
using FirewallWidget.Manager.DTO;
using System.Drawing;
using System.IO;

namespace FirewallWidget.Manager.MappingProfiles
{
    internal class RuleProfile : Profile
    {
        private readonly ImageConverter converter = new ImageConverter();

        public RuleProfile()
        {
            CreateMap<RuleDto, Rule>()
                .ForMember(r => r.Icon, opts => opts.MapFrom((dto, r) =>
                {
                    return dto.Icon != null ? (byte[])converter.ConvertTo(dto.Icon, typeof(byte[])) : (byte[])null;
                }))
                .ReverseMap()
                .ForMember(r => r.Icon, opts => opts.MapFrom((r, dto) =>
                {
                    return r.Icon != null && r.Icon.Length > 0 ? new Bitmap(new MemoryStream(r.Icon)) : null;
                }));
        }
    }
}
