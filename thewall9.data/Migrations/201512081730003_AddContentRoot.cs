namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContentRoot : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContentRoots",
                c => new
                    {
                        ContentID = c.Int(nullable: false),
                        ContentParentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ContentID, t.ContentParentID })
                .ForeignKey("dbo.ContentProperties", t => t.ContentID, cascadeDelete: true)
                .Index(t => t.ContentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContentRoots", "ContentID", "dbo.ContentProperties");
            DropIndex("dbo.ContentRoots", new[] { "ContentID" });
            DropTable("dbo.ContentRoots");
        }
    }
}
