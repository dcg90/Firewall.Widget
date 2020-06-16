using FirewallWidget.Data;

namespace FirewallWidget.DataAccess.Contracts.Repositories
{
    public interface IOptionsRepository
    {
        Options ReadOptions();

        void UpdateOptions(Options options);
    }
}
