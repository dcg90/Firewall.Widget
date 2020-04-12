namespace FirewallWidget.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addruledirection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rules", "Direction", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rules", "Direction");
        }
    }
}
