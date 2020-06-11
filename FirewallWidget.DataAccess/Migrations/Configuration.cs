namespace FirewallWidget.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<FirewallWidget.DataAccess.Contexts.SQLServerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "FirewallWidget.DataAccess.Contexts.EFDbContext";
        }

        protected override void Seed(FirewallWidget.DataAccess.Contexts.SQLServerDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
