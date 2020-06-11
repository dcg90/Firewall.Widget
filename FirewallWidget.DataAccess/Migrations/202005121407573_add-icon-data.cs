namespace FirewallWidget.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class addicondata : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rules", "Icon", c => c.Binary());
        }

        public override void Down()
        {
            DropColumn("dbo.Rules", "Icon");
        }
    }
}
