using FirewallWidget.Data;
using FirewallWidget.DataAccess.Contracts.Context;

using System.Data.Entity;

namespace FirewallWidget.DataAccess.Contexts
{
    public class EFDbContext : DbContext, IEFDbContext
    {
        public EFDbContext(string conn)
            : base(conn)
        { }

        public DbSet<Rule> Rules { get; set; }

        public DbContext Context => this;

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rule>().CreateModel();
            modelBuilder.Entity<Options>().CreateModel();

            base.OnModelCreating(modelBuilder);
        }
    }
}
