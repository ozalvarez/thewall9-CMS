namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addproductcurrencyprice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCurrencies", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductCurrencies", "Price");
        }
    }
}
