namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlogCategoryAddSiteID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BlogCategoryCultures", "CultureID", "dbo.Cultures");
            AddColumn("dbo.BlogCategories", "SiteID", c => c.Int(nullable: false));
            AddForeignKey("dbo.BlogCategoryCultures", "CultureID", "dbo.Cultures", "CultureID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogCategoryCultures", "CultureID", "dbo.Cultures");
            DropColumn("dbo.BlogCategories", "SiteID");
            AddForeignKey("dbo.BlogCategoryCultures", "CultureID", "dbo.Cultures", "CultureID", cascadeDelete: true);
        }
    }
}
