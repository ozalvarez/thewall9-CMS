namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtag : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagID = c.Int(nullable: false, identity: true),
                        TagName = c.String(),
                        SiteID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TagID)
                .ForeignKey("dbo.Sites", t => t.SiteID, cascadeDelete: true)
                .Index(t => t.SiteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "SiteID", "dbo.Sites");
            DropIndex("dbo.Tags", new[] { "SiteID" });
            DropTable("dbo.Tags");
        }
    }
}
