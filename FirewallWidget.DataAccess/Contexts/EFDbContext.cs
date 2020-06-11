using FirewallWidget.Data;
using FirewallWidget.DataAccess.Contracts.Context;

using System.ComponentModel.DataAnnotations.Schema;
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
            modelBuilder.Entity<Rule>()
               .HasKey(r => r.Id);
            modelBuilder.Entity<Rule>()
                .Property(r => r.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            base.OnModelCreating(modelBuilder);
        }
    }
}
