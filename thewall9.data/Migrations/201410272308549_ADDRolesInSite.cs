namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDRolesInSite : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sites", "UserIDOwner", "dbo.AspNetUsers");
            DropIndex("dbo.Sites", new[] { "UserIDOwner" });
            CreateTable(
                "dbo.SiteUsers",
                c => new
                    {
                        SiteID = c.Int(nullable: false),
                        UserID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.SiteID, t.UserID })
                .ForeignKey("dbo.Sites", t => t.SiteID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.SiteID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.SiteUserRols",
                c => new
                    {
                        SiteID = c.Int(nullable: false),
                        UserID = c.String(nullable: false, maxLength: 128),
                        SiteUserType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SiteID, t.UserID, t.SiteUserType })
                .ForeignKey("dbo.SiteUsers", t => new { t.SiteID, t.UserID }, cascadeDelete: true)
                .Index(t => new { t.SiteID, t.UserID });
            
            AddColumn("dbo.Sites", "Enabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Sites", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            DropColumn("dbo.Sites", "UserIDOwner");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sites", "UserIDOwner", c => c.String(maxLength: 128));
            DropForeignKey("dbo.SiteUsers", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.SiteUserRols", new[] { "SiteID", "UserID" }, "dbo.SiteUsers");
            DropForeignKey("dbo.SiteUsers", "SiteID", "dbo.Sites");
            DropIndex("dbo.SiteUserRols", new[] { "SiteID", "UserID" });
            DropIndex("dbo.SiteUsers", new[] { "UserID" });
            DropIndex("dbo.SiteUsers", new[] { "SiteID" });
            DropColumn("dbo.AspNetUsers", "Name");
            DropColumn("dbo.Sites", "DateCreated");
            DropColumn("dbo.Sites", "Enabled");
            DropTable("dbo.SiteUserRols");
            DropTable("dbo.SiteUsers");
            CreateIndex("dbo.Sites", "UserIDOwner");
            AddForeignKey("dbo.Sites", "UserIDOwner", "dbo.AspNetUsers", "Id");
        }
    }
}
