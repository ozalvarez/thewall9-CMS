namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImagesToBlog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogPostImages",
                c => new
                    {
                        BlogPostID = c.Int(nullable: false),
                        CultureID = c.Int(nullable: false),
                        MediaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BlogPostID, t.CultureID, t.MediaID })
                .ForeignKey("dbo.BlogPostCultures", t => new { t.BlogPostID, t.CultureID }, cascadeDelete: true)
                .ForeignKey("dbo.Media", t => t.MediaID, cascadeDelete: true)
                .Index(t => new { t.BlogPostID, t.CultureID })
                .Index(t => t.MediaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogPostImages", "MediaID", "dbo.Media");
            DropForeignKey("dbo.BlogPostImages", new[] { "BlogPostID", "CultureID" }, "dbo.BlogPostCultures");
            DropIndex("dbo.BlogPostImages", new[] { "MediaID" });
            DropIndex("dbo.BlogPostImages", new[] { "BlogPostID", "CultureID" });
            DropTable("dbo.BlogPostImages");
        }
    }
}
