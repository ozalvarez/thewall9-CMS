namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlogCategoryAddSiteID2 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.BlogCategories", "SiteID");
            AddForeignKey("dbo.BlogCategories", "SiteID", "dbo.Sites", "SiteID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogCategories", "SiteID", "dbo.Sites");
            DropIndex("dbo.BlogCategories", new[] { "SiteID" });
        }
    }
}
