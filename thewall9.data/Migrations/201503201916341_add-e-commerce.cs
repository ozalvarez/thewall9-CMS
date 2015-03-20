namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addecommerce : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sites", "ECommerce", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sites", "ECommerce");
        }
    }
}
