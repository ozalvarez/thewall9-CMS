namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfriendlyurlincategoryculture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CategoryCultures", "FriendlyUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CategoryCultures", "FriendlyUrl");
        }
    }
}
