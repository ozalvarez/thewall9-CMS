namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addshippingprice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Currencies", "ShippingPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Currencies", "ShippingPrice");
        }
    }
}
