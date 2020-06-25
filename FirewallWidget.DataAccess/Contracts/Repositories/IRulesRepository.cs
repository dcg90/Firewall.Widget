using FirewallWidget.Data;

using System.Collections.Generic;

namespace FirewallWidget.DataAccess.Contracts.Repositories
{
    public interface IRulesRepository : IRepository<Rule, int>
    {
        bool RuleExist(string name, int profile, int direction);

        IEnumerable<Rule> Read(string name, int profile, int direction);
    }
}
