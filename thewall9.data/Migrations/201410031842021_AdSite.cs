namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdSite : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        SiteID = c.Int(nullable: false, identity: true),
                        DefaultLang = c.String(),
                        SiteName = c.String(),
                        GAID = c.String(),
                        UserIDOwner = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SiteID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserIDOwner)
                .Index(t => t.UserIDOwner);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sites", "UserIDOwner", "dbo.AspNetUsers");
            DropIndex("dbo.Sites", new[] { "UserIDOwner" });
            DropTable("dbo.Sites");
        }
    }
}
