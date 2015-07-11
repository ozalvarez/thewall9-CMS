namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRssToBlog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cultures", "Rss", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cultures", "Rss");
        }
    }
}
