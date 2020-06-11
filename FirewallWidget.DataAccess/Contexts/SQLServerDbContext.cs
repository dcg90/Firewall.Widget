namespace FirewallWidget.DataAccess.Contexts
{
    public class SQLServerDbContext : EFDbContext
    {
        public SQLServerDbContext()
            : base("SQLServer_ManagerDb")
        { }
    }
}
