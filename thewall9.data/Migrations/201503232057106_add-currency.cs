namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcurrency : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        CurrencyID = c.Int(nullable: false, identity: true),
                        CurrencyName = c.String(),
                        Default = c.Boolean(nullable: false),
                        SiteID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CurrencyID)
                .ForeignKey("dbo.Sites", t => t.SiteID, cascadeDelete: true)
                .Index(t => t.SiteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Currencies", "SiteID", "dbo.Sites");
            DropIndex("dbo.Currencies", new[] { "SiteID" });
            DropTable("dbo.Currencies");
        }
    }
}
