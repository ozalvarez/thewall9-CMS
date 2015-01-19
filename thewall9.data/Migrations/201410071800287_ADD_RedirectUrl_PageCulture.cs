namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_RedirectUrl_PageCulture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PageCultures", "RedirectUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PageCultures", "RedirectUrl");
        }
    }
}
