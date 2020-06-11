﻿using FirewallWidget.Data;
using FirewallWidget.DataAccess.Contracts.Context;
using FirewallWidget.DataAccess.Contracts.Repositories;

using System.Linq;

namespace FirewallWidget.DataAccess.Repositories.EF
{
    public class RulesRepository
        : BaseRepository<Rule, int>,
        IRulesRepository
    {
        public RulesRepository(IEFDbContext dbContext)
            : base(dbContext, dbContext.Rules)
        { }

        public bool RuleExist(string name)
        {
            var q = from r in dbContext.Rules
                    where r.Name == name
                    select r;

            return q.FirstOrDefault() != null;
        }
    }
}
