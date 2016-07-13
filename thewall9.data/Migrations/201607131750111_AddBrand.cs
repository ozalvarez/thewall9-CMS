namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBrand : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        BrandID = c.Int(nullable: false, identity: true),
                        BrandName = c.String(),
                        BrandDescription = c.String(),
                        IconUrl = c.String(),
                        SiteID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BrandID)
                .ForeignKey("dbo.Sites", t => t.SiteID, cascadeDelete: true)
                .Index(t => t.SiteID);
            
            AddColumn("dbo.Products", "BrandID", c => c.Int());
            CreateIndex("dbo.Products", "BrandID");
            AddForeignKey("dbo.Products", "BrandID", "dbo.Brands", "BrandID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Brands", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.Products", "BrandID", "dbo.Brands");
            DropIndex("dbo.Products", new[] { "BrandID" });
            DropIndex("dbo.Brands", new[] { "SiteID" });
            DropColumn("dbo.Products", "BrandID");
            DropTable("dbo.Brands");
        }
    }
}
