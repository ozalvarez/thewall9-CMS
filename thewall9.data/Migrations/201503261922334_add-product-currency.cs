namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addproductcurrency : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductCurrencies",
                c => new
                    {
                        ProductID = c.Int(nullable: false),
                        CurrencyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductID, t.CurrencyID })
                .ForeignKey("dbo.Currencies", t => t.CurrencyID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.CurrencyID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductCurrencies", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductCurrencies", "CurrencyID", "dbo.Currencies");
            DropIndex("dbo.ProductCurrencies", new[] { "CurrencyID" });
            DropIndex("dbo.ProductCurrencies", new[] { "ProductID" });
            DropTable("dbo.ProductCurrencies");
        }
    }
}
