namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFriendlyUrlToCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogCategoryCultures", "FriendlyUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BlogCategoryCultures", "FriendlyUrl");
        }
    }
}
