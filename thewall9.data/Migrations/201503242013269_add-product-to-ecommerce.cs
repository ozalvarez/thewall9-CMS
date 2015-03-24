namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addproducttoecommerce : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        ProductID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductID, t.CategoryID })
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        SiteID = c.Int(nullable: false),
                        ProductAlias = c.String(),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Sites", t => t.SiteID, cascadeDelete: false)
                .Index(t => t.SiteID);
            
            CreateTable(
                "dbo.ProductCultures",
                c => new
                    {
                        ProductID = c.Int(nullable: false),
                        CultureID = c.Int(nullable: false),
                        ProductName = c.String(),
                        Description = c.String(),
                        AdditionalInformation = c.String(),
                        IconPath = c.String(),
                        FriendlyUrl = c.String(),
                    })
                .PrimaryKey(t => new { t.ProductID, t.CultureID })
                .ForeignKey("dbo.Cultures", t => t.CultureID, cascadeDelete: false)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.CultureID);
            
            CreateTable(
                "dbo.ProductGalleries",
                c => new
                    {
                        ProductGalleryID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        PhotoPath = c.String(),
                    })
                .PrimaryKey(t => t.ProductGalleryID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.ProductTags",
                c => new
                    {
                        ProductID = c.Int(nullable: false),
                        TagID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductID, t.TagID })
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.TagID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductTags", "TagID", "dbo.Tags");
            DropForeignKey("dbo.ProductTags", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductGalleries", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductCategories", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.ProductCultures", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductCultures", "CultureID", "dbo.Cultures");
            DropForeignKey("dbo.ProductCategories", "CategoryID", "dbo.Categories");
            DropIndex("dbo.ProductTags", new[] { "TagID" });
            DropIndex("dbo.ProductTags", new[] { "ProductID" });
            DropIndex("dbo.ProductGalleries", new[] { "ProductID" });
            DropIndex("dbo.ProductCultures", new[] { "CultureID" });
            DropIndex("dbo.ProductCultures", new[] { "ProductID" });
            DropIndex("dbo.Products", new[] { "SiteID" });
            DropIndex("dbo.ProductCategories", new[] { "CategoryID" });
            DropIndex("dbo.ProductCategories", new[] { "ProductID" });
            DropTable("dbo.ProductTags");
            DropTable("dbo.ProductGalleries");
            DropTable("dbo.ProductCultures");
            DropTable("dbo.Products");
            DropTable("dbo.ProductCategories");
        }
    }
}
