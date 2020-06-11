using SQLite.CodeFirst;
using System.Data.Entity;

namespace FirewallWidget.DataAccess.Contexts
{
    public class SQLiteDbContext : EFDbContext
    {
        public SQLiteDbContext()
            : base("SQLite_ManagerDb")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sQLiteInitializer = new SqliteDropCreateDatabaseWhenModelChanges<SQLiteDbContext>(modelBuilder);
            Database.SetInitializer(sQLiteInitializer);

            base.OnModelCreating(modelBuilder);
        }
    }
}
