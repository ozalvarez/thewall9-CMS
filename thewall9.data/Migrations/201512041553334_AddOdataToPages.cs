namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOdataToPages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PageCultureOGraphs",
                c => new
                    {
                        PageID = c.Int(nullable: false),
                        CultureID = c.Int(nullable: false),
                        OGraphID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PageID, t.CultureID })
                .ForeignKey("dbo.OGraphs", t => t.OGraphID, cascadeDelete: true)
                .ForeignKey("dbo.PageCultures", t => new { t.PageID, t.CultureID }, cascadeDelete: true)
                .Index(t => new { t.PageID, t.CultureID })
                .Index(t => t.OGraphID);
            
            CreateTable(
                "dbo.OGraphs",
                c => new
                    {
                        OGraphID = c.Int(nullable: false, identity: true),
                        OGraphTitle = c.String(),
                        OGraphDescription = c.String(),
                    })
                .PrimaryKey(t => t.OGraphID);
            
            CreateTable(
                "dbo.OGraphMedias",
                c => new
                    {
                        OGraphID = c.Int(nullable: false),
                        MediaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OGraphID)
                .ForeignKey("dbo.Media", t => t.MediaID, cascadeDelete: true)
                .ForeignKey("dbo.OGraphs", t => t.OGraphID, cascadeDelete: true)
                .Index(t => t.OGraphID)
                .Index(t => t.MediaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PageCultureOGraphs", new[] { "PageID", "CultureID" }, "dbo.PageCultures");
            DropForeignKey("dbo.PageCultureOGraphs", "OGraphID", "dbo.OGraphs");
            DropForeignKey("dbo.OGraphMedias", "OGraphID", "dbo.OGraphs");
            DropForeignKey("dbo.OGraphMedias", "MediaID", "dbo.Media");
            DropIndex("dbo.OGraphMedias", new[] { "MediaID" });
            DropIndex("dbo.OGraphMedias", new[] { "OGraphID" });
            DropIndex("dbo.PageCultureOGraphs", new[] { "OGraphID" });
            DropIndex("dbo.PageCultureOGraphs", new[] { "PageID", "CultureID" });
            DropTable("dbo.OGraphMedias");
            DropTable("dbo.OGraphs");
            DropTable("dbo.PageCultureOGraphs");
        }
    }
}
