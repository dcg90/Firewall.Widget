using FirewallWidget.Data;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace FirewallWidget.DataAccess.Contexts
{
    public class EFDbContext : DbContext
    {
        public EFDbContext()
            : base("ManagerDb")
        { }

        public DbSet<Rule> Rules { get; set; }

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
