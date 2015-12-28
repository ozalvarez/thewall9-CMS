namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPriceSale : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCurrencies", "PriceSale", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductCurrencies", "PriceSale");
        }
    }
}
