namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSiteIDToMedia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Media", "SiteID", c => c.Int(nullable: true));
            CreateIndex("dbo.Media", "SiteID");
            AddForeignKey("dbo.Media", "SiteID", "dbo.Sites", "SiteID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Media", "SiteID", "dbo.Sites");
            DropIndex("dbo.Media", new[] { "SiteID" });
            DropColumn("dbo.Media", "SiteID");
        }
    }
}
