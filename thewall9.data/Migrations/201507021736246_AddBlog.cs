namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBlog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogCategories",
                c => new
                    {
                        BlogCategoryID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.BlogCategoryID);
            
            CreateTable(
                "dbo.BlogCategoryCultures",
                c => new
                    {
                        BlogCategoryID = c.Int(nullable: false),
                        CultureID = c.Int(nullable: false),
                        BlogCategoryName = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BlogCategoryID, t.CultureID })
                .ForeignKey("dbo.BlogCategories", t => t.BlogCategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Cultures", t => t.CultureID, cascadeDelete: true)
                .Index(t => t.BlogCategoryID)
                .Index(t => t.CultureID);
            
            CreateTable(
                "dbo.BlogPostCultures",
                c => new
                    {
                        BlogPostID = c.Int(nullable: false),
                        CultureID = c.Int(nullable: false),
                        Published = c.Boolean(nullable: false),
                        Content = c.String(),
                        FriendlyUrl = c.String(),
                        Title = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.BlogPostID, t.CultureID })
                .ForeignKey("dbo.BlogPosts", t => t.BlogPostID, cascadeDelete: true)
                .ForeignKey("dbo.Cultures", t => t.CultureID)
                .Index(t => t.BlogPostID)
                .Index(t => t.CultureID);
            
            CreateTable(
                "dbo.BlogPosts",
                c => new
                    {
                        BlogPostID = c.Int(nullable: false, identity: true),
                        SiteID = c.Int(nullable: false),
                        Published = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BlogPostID)
                .ForeignKey("dbo.Sites", t => t.SiteID, cascadeDelete: true)
                .Index(t => t.SiteID);
            
            CreateTable(
                "dbo.BlogPostCategories",
                c => new
                    {
                        BlogPostID = c.Int(nullable: false),
                        BlogCategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BlogPostID, t.BlogCategoryID })
                .ForeignKey("dbo.BlogCategories", t => t.BlogCategoryID, cascadeDelete: true)
                .ForeignKey("dbo.BlogPosts", t => t.BlogPostID, cascadeDelete: true)
                .Index(t => t.BlogPostID)
                .Index(t => t.BlogCategoryID);
            
            CreateTable(
                "dbo.BlogPostTags",
                c => new
                    {
                        BlogTagID = c.Int(nullable: false),
                        BlogPostID = c.Int(nullable: false),
                        CultureID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BlogTagID, t.BlogPostID, t.CultureID })
                .ForeignKey("dbo.BlogPostCultures", t => new { t.BlogPostID, t.CultureID }, cascadeDelete: true)
                .ForeignKey("dbo.BlogTags", t => t.BlogTagID, cascadeDelete: true)
                .Index(t => t.BlogTagID)
                .Index(t => new { t.BlogPostID, t.CultureID });
            
            CreateTable(
                "dbo.BlogTags",
                c => new
                    {
                        BlogTagID = c.Int(nullable: false, identity: true),
                        BlogTagName = c.String(),
                    })
                .PrimaryKey(t => t.BlogTagID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogPostTags", "BlogTagID", "dbo.BlogTags");
            DropForeignKey("dbo.BlogPostTags", new[] { "BlogPostID", "CultureID" }, "dbo.BlogPostCultures");
            DropForeignKey("dbo.BlogPostCategories", "BlogPostID", "dbo.BlogPosts");
            DropForeignKey("dbo.BlogPostCategories", "BlogCategoryID", "dbo.BlogCategories");
            DropForeignKey("dbo.BlogCategoryCultures", "CultureID", "dbo.Cultures");
            DropForeignKey("dbo.BlogPostCultures", "CultureID", "dbo.Cultures");
            DropForeignKey("dbo.BlogPostCultures", "BlogPostID", "dbo.BlogPosts");
            DropForeignKey("dbo.BlogPosts", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.BlogCategoryCultures", "BlogCategoryID", "dbo.BlogCategories");
            DropIndex("dbo.BlogPostTags", new[] { "BlogPostID", "CultureID" });
            DropIndex("dbo.BlogPostTags", new[] { "BlogTagID" });
            DropIndex("dbo.BlogPostCategories", new[] { "BlogCategoryID" });
            DropIndex("dbo.BlogPostCategories", new[] { "BlogPostID" });
            DropIndex("dbo.BlogPosts", new[] { "SiteID" });
            DropIndex("dbo.BlogPostCultures", new[] { "CultureID" });
            DropIndex("dbo.BlogPostCultures", new[] { "BlogPostID" });
            DropIndex("dbo.BlogCategoryCultures", new[] { "CultureID" });
            DropIndex("dbo.BlogCategoryCultures", new[] { "BlogCategoryID" });
            DropTable("dbo.BlogTags");
            DropTable("dbo.BlogPostTags");
            DropTable("dbo.BlogPostCategories");
            DropTable("dbo.BlogPosts");
            DropTable("dbo.BlogPostCultures");
            DropTable("dbo.BlogCategoryCultures");
            DropTable("dbo.BlogCategories");
        }
    }
}
