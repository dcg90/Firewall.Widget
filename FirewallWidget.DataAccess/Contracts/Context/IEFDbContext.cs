
using System.Data.Entity;

namespace FirewallWidget.DataAccess.Contracts.Context
{
    public interface IEFDbContext
    {
        DbContext Context { get; }
    }
}