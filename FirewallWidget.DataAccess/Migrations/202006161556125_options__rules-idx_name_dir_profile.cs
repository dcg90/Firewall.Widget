namespace FirewallWidget.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class options__rulesidx_name_dir_profile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Options",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OverrideRules = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Rules", "Name", c => c.String(maxLength: 500));
            CreateIndex("dbo.Rules", new[] { "Name", "Direction", "Profile" });
        }
        
        public override void Down()
        {
            DropIndex("dbo.Rules", new[] { "Name", "Direction", "Profile" });
            AlterColumn("dbo.Rules", "Name", c => c.String());
            DropTable("dbo.Options");
        }
    }
}
