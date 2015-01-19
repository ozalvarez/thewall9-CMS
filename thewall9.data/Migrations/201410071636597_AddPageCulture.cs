namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPageCulture : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PageCultures",
                c => new
                    {
                        PageID = c.Int(nullable: false),
                        CultureID = c.Int(nullable: false),
                        TitlePage = c.String(),
                        MetaDescription = c.String(),
                        FriendlyUrl = c.String(),
                        ViewRender = c.String(),
                        Published = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.PageID, t.CultureID })
                .ForeignKey("dbo.Pages", t => t.PageID, cascadeDelete: true)
                .ForeignKey("dbo.Cultures", t => t.CultureID)
                .Index(t => t.PageID)
                .Index(t => t.CultureID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PageCultures", "PageID", "dbo.Pages");
            DropForeignKey("dbo.PageCultures", "CultureID", "dbo.Cultures");
            DropIndex("dbo.PageCultures", new[] { "CultureID" });
            DropIndex("dbo.PageCultures", new[] { "PageID" });
            DropTable("dbo.PageCultures");
        }
    }
}
