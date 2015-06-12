namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEnabledToContent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContentProperties", "Enabled", c => c.Boolean(nullable: false,defaultValue:true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContentProperties", "Enabled");
        }
    }
}
