using FirewallWidget.Data;

using System.Data.Entity;

namespace FirewallWidget.DataAccess.Contracts.Context
{
    public interface IEFDbContext
    {
        DbSet<Rule> Rules { get; }

        DbContext Context { get; }
    }
}