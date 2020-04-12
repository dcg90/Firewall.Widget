using FirewallWidget.Data;

namespace FirewallWidget.DataAccess.Contracts.Repositories
{
    public interface IRulesRepository : IRepository<Rule, int>
    {
        bool RuleExist(string name);
    }
}
