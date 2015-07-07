namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMedia : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogPostFeatureImages",
                c => new
                    {
                        BlogPostID = c.Int(nullable: false),
                        CultureID = c.Int(nullable: false),
                        MediaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BlogPostID, t.CultureID })
                .ForeignKey("dbo.BlogPostCultures", t => new { t.BlogPostID, t.CultureID }, cascadeDelete: true)
                .ForeignKey("dbo.Media", t => t.MediaID, cascadeDelete: true)
                .Index(t => new { t.BlogPostID, t.CultureID })
                .Index(t => t.MediaID);
            
            CreateTable(
                "dbo.Media",
                c => new
                    {
                        MediaID = c.Int(nullable: false, identity: true),
                        MediaUrl = c.String(),
                    })
                .PrimaryKey(t => t.MediaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogPostFeatureImages", "MediaID", "dbo.Media");
            DropForeignKey("dbo.BlogPostFeatureImages", new[] { "BlogPostID", "CultureID" }, "dbo.BlogPostCultures");
            DropIndex("dbo.BlogPostFeatureImages", new[] { "MediaID" });
            DropIndex("dbo.BlogPostFeatureImages", new[] { "BlogPostID", "CultureID" });
            DropTable("dbo.Media");
            DropTable("dbo.BlogPostFeatureImages");
        }
    }
}
