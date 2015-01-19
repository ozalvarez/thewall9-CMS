namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSiteUrl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SiteUrls",
                c => new
                    {
                        SiteUrlID = c.Int(nullable: false, identity: true),
                        SiteID = c.Int(nullable: false),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.SiteUrlID)
                .ForeignKey("dbo.Sites", t => t.SiteID, cascadeDelete: true)
                .Index(t => t.SiteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SiteUrls", "SiteID", "dbo.Sites");
            DropIndex("dbo.SiteUrls", new[] { "SiteID" });
            DropTable("dbo.SiteUrls");
        }
    }
}
