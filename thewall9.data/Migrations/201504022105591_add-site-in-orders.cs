namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsiteinorders : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Orders", "SiteID");
            AddForeignKey("dbo.Orders", "SiteID", "dbo.Sites", "SiteID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "SiteID", "dbo.Sites");
            DropIndex("dbo.Orders", new[] { "SiteID" });
        }
    }
}
