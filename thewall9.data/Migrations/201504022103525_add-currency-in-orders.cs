namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcurrencyinorders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CurrencyID", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "CurrencyID");
            AddForeignKey("dbo.Orders", "CurrencyID", "dbo.Currencies", "CurrencyID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CurrencyID", "dbo.Currencies");
            DropIndex("dbo.Orders", new[] { "CurrencyID" });
            DropColumn("dbo.Orders", "CurrencyID");
        }
    }
}
