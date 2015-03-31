namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmoneysymbolproducts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Currencies", "MoneySymbol", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Currencies", "MoneySymbol");
        }
    }
}
