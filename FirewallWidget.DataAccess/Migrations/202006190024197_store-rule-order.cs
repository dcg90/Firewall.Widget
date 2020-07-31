namespace FirewallWidget.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class storeruleorder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rules", "Order", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Rules", "Order");
        }
    }
}
