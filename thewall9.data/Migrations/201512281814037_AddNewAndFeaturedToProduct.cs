namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewAndFeaturedToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "New", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "Featured", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Featured");
            DropColumn("dbo.Products", "New");
        }
    }
}
