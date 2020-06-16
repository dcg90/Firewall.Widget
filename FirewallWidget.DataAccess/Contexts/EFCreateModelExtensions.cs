using FirewallWidget.Data;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FirewallWidget.DataAccess.Contexts
{
    internal static class EFCreateModelExtensions
    {
        public static void CreateModel(this EntityTypeConfiguration<Rule> config)
        {
            config.HasKey(r => r.Id);
            config
                .Property(r => r.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            config.Property(r => r.Name)
                .HasMaxLength(500);
            config.HasIndex(r => new { r.Name, r.Direction, r.Profile });
        }

        public static void CreateModel(this EntityTypeConfiguration<Options> config)
        {
            config.HasKey(o => o.Id);
            config
                .Property(o => o.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

    }
}
