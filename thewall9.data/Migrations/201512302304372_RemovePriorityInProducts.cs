namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePriorityInProducts : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "Priority");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Priority", c => c.Int(nullable: false));
        }
    }
}
