using FirewallWidget.Data;
using FirewallWidget.DataAccess.Contracts.Context;
using FirewallWidget.DataAccess.Contracts.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FirewallWidget.DataAccess.Repositories.EF
{
    public class RulesRepository
        : BaseRepository<Rule, int>,
        IRulesRepository
    {
        public RulesRepository(IEFDbContext dbContext)
            : base(dbContext, dbContext.Context.Set<Rule>())
        { }

        public IEnumerable<Rule> Read(string name, int profile, int direction)
        {
            return Entities
                .Where(RuleNameProfileDirection(name, profile, direction));
        }

        public bool RuleExist(string name, int profile, int direction)
        {
            return Entities
                .Where(RuleNameProfileDirection(name, profile, direction))
                .FirstOrDefault() != null;
        }

        private static Expression<Func<Rule, bool>> RuleNameProfileDirection(
            string name, int profile, int direction)
        {
            return (r) => r.Name == name &&
                          r.Profile == profile &&
                          r.Direction == direction;
        }
    }
}
