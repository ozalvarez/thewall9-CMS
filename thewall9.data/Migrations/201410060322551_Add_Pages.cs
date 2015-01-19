namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Pages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        PageID = c.Int(nullable: false, identity: true),
                        Alias = c.String(),
                        Published = c.Boolean(nullable: false),
                        Priority = c.Int(nullable: false),
                        InMenu = c.Boolean(nullable: false),
                        SiteID = c.Int(nullable: false),
                        PageParentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PageID)
                .ForeignKey("dbo.Sites", t => t.SiteID, cascadeDelete: true)
                .Index(t => t.SiteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pages", "SiteID", "dbo.Sites");
            DropIndex("dbo.Pages", new[] { "SiteID" });
            DropTable("dbo.Pages");
        }
    }
}
