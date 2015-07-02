namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBlog : DbMigration
    {
        public override void Up()
        {
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
                        UserIDCreator = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BlogPostID)
                .ForeignKey("dbo.Sites", t => t.SiteID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserIDCreator)
                .Index(t => t.SiteID)
                .Index(t => t.UserIDCreator);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogPostCultures", "CultureID", "dbo.Cultures");
            DropForeignKey("dbo.BlogPostCultures", "BlogPostID", "dbo.BlogPosts");
            DropForeignKey("dbo.BlogPosts", "UserIDCreator", "dbo.AspNetUsers");
            DropForeignKey("dbo.BlogPosts", "SiteID", "dbo.Sites");
            DropIndex("dbo.BlogPosts", new[] { "UserIDCreator" });
            DropIndex("dbo.BlogPosts", new[] { "SiteID" });
            DropIndex("dbo.BlogPostCultures", new[] { "CultureID" });
            DropIndex("dbo.BlogPostCultures", new[] { "BlogPostID" });
            DropTable("dbo.BlogPosts");
            DropTable("dbo.BlogPostCultures");
        }
    }
}
