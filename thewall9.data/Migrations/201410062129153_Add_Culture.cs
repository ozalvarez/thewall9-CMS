namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Culture : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cultures",
                c => new
                    {
                        CultureID = c.Int(nullable: false, identity: true),
                        SiteID = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CultureID)
                .ForeignKey("dbo.Sites", t => t.SiteID, cascadeDelete: true)
                .Index(t => t.SiteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cultures", "SiteID", "dbo.Sites");
            DropIndex("dbo.Cultures", new[] { "SiteID" });
            DropTable("dbo.Cultures");
        }
    }
}
