namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CahngToPriceOld : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCurrencies", "PriceOld", c => c.Double(nullable: false));
            DropColumn("dbo.ProductCurrencies", "PriceSale");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductCurrencies", "PriceSale", c => c.Double(nullable: false));
            DropColumn("dbo.ProductCurrencies", "PriceOld");
        }
    }
}
