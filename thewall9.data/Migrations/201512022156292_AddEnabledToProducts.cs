namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEnabledToProducts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Enabled", c => c.Boolean(nullable: false,defaultValue:true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Enabled");
        }
    }
}
