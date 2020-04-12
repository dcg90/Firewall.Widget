namespace FirewallWidget.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addprofile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rules", "Profile", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rules", "Profile");
        }
    }
}
