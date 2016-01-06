namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class PutSiteIDInMediaRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Media", "SiteID", "dbo.Sites");
            DropIndex("dbo.Media", new[] { "SiteID" });
            AlterColumn("dbo.Media", "SiteID", c => c.Int(nullable: false));
            CreateIndex("dbo.Media", "SiteID");
            AddForeignKey("dbo.Media", "SiteID", "dbo.Sites", "SiteID");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Media", "SiteID", "dbo.Sites");
            DropIndex("dbo.Media", new[] { "SiteID" });
            AlterColumn("dbo.Media", "SiteID", c => c.Int(nullable: true));
            CreateIndex("dbo.Media", "SiteID");
            AddForeignKey("dbo.Media", "SiteID", "dbo.Sites", "SiteID", cascadeDelete: true);
        }
    }
}
