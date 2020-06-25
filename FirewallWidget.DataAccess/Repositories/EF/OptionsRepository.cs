using FirewallWidget.Data;
using FirewallWidget.DataAccess.Contracts.Context;
using FirewallWidget.DataAccess.Contracts.Repositories;

using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace FirewallWidget.DataAccess.Repositories.EF
{
    public class OptionsRepository : IOptionsRepository
    {
        private readonly DbSet<Options> options;
        private readonly DbContext dbContext;

        public OptionsRepository(IEFDbContext dbContext)
        {
            options = dbContext.Context.Set<Options>();
            this.dbContext = dbContext.Context;
        }

        public Options ReadOptions()
        {
            return options.AsNoTracking().FirstOrDefault();
        }

        public void UpdateOptions(Options options)
        {
            this.options.AddOrUpdate(options);
            dbContext.SaveChanges();
        }
    }
}
