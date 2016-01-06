namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePublishedInPage : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Pages", "Published");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pages", "Published", c => c.Boolean(nullable: false));
        }
    }
}
