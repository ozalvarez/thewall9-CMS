namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpriorityonproducts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Priority", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Priority");
        }
    }
}
