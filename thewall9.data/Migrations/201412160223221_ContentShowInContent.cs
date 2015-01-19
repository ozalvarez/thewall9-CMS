namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContentShowInContent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContentProperties", "ShowInContent", c => c.Boolean(nullable: false,defaultValue:false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContentProperties", "ShowInContent");
        }
    }
}
