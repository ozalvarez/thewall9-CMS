namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContentProperties",
                c => new
                    {
                        ContentPropertyID = c.Int(nullable: false, identity: true),
                        ContentPropertyParentID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        ContentPropertyType = c.Int(nullable: false),
                        ContentPropertyAlias = c.String(),
                        Priority = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContentPropertyID)
                .ForeignKey("dbo.Sites", t => t.SiteID, cascadeDelete: true)
                .Index(t => t.SiteID);
            
            CreateTable(
                "dbo.ContentPropertyCultures",
                c => new
                    {
                        ContentPropertyID = c.Int(nullable: false),
                        CultureID = c.Int(nullable: false),
                        ContentPropertyValue = c.String(),
                    })
                .PrimaryKey(t => new { t.ContentPropertyID, t.CultureID })
                .ForeignKey("dbo.ContentProperties", t => t.ContentPropertyID, cascadeDelete: true)
                .ForeignKey("dbo.Cultures", t => t.CultureID)
                .Index(t => t.ContentPropertyID)
                .Index(t => t.CultureID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContentPropertyCultures", "CultureID", "dbo.Cultures");
            DropForeignKey("dbo.ContentPropertyCultures", "ContentPropertyID", "dbo.ContentProperties");
            DropForeignKey("dbo.ContentProperties", "SiteID", "dbo.Sites");
            DropIndex("dbo.ContentPropertyCultures", new[] { "CultureID" });
            DropIndex("dbo.ContentPropertyCultures", new[] { "ContentPropertyID" });
            DropIndex("dbo.ContentProperties", new[] { "SiteID" });
            DropTable("dbo.ContentPropertyCultures");
            DropTable("dbo.ContentProperties");
        }
    }
}
