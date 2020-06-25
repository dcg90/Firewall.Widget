namespace FirewallWidget.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class docking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Options", "DockLeft", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Options", "DockLeft");
        }
    }
}
