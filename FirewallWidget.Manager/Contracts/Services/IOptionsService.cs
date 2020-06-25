using FirewallWidget.Manager.DTO;

namespace FirewallWidget.Manager.Contracts.Services
{
    public interface IOptionsService
    {
        OptionsDto ReadOptions();

        void UpdateOptions(OptionsDto options);
    }
}
