namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPriorityToCategoryInProducts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCategories", "Priority", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductCategories", "Priority");
        }
    }
}
