using AutoMapper;

using FirewallWidget.Data;
using FirewallWidget.DataAccess.Contracts.Repositories;
using FirewallWidget.Manager.Contracts.Services;
using FirewallWidget.Manager.DTO;

namespace FirewallWidget.Manager.Services
{
    public class OptionsService : IOptionsService
    {
        private readonly IOptionsRepository optionsRepository;
        private readonly IMapper mapper;

        public OptionsService(IOptionsRepository optionsRepository, IMapper mapper)
        {
            this.optionsRepository = optionsRepository;
            this.mapper = mapper;
        }

        public OptionsDto ReadOptions()
        {
            var options = optionsRepository.ReadOptions();
            return options != null
                ? mapper.Map<OptionsDto>(options)
                : null;
        }

        public void UpdateOptions(OptionsDto options)
        {
            var optionsDb = optionsRepository.ReadOptions();
            optionsRepository.UpdateOptions(
                optionsDb != null
                    ? (Options)mapper.Map(
                        options, optionsDb, typeof(OptionsDto), typeof(Options))
                    : mapper.Map<Options>(options));
        }
    }
}
