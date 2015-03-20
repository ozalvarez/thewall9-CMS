namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcategorytoecommerce : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        SiteID = c.Int(nullable: false),
                        CategoryAlias = c.String(),
                        Priority = c.Int(nullable: false),
                        CategoryParentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID)
                .ForeignKey("dbo.Sites", t => t.SiteID, cascadeDelete: true)
                .Index(t => t.SiteID);
            
            CreateTable(
                "dbo.CategoryCultures",
                c => new
                    {
                        CategoryID = c.Int(nullable: false),
                        CultureID = c.Int(nullable: false),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => new { t.CategoryID, t.CultureID })
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Cultures", t => t.CultureID, cascadeDelete: false)
                .Index(t => t.CategoryID)
                .Index(t => t.CultureID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.CategoryCultures", "CultureID", "dbo.Cultures");
            DropForeignKey("dbo.CategoryCultures", "CategoryID", "dbo.Categories");
            DropIndex("dbo.CategoryCultures", new[] { "CultureID" });
            DropIndex("dbo.CategoryCultures", new[] { "CategoryID" });
            DropIndex("dbo.Categories", new[] { "SiteID" });
            DropTable("dbo.CategoryCultures");
            DropTable("dbo.Categories");
        }
    }
}
