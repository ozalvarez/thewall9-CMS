namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSiteUrl : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Sites", "Url");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sites", "Url", c => c.String());
        }
    }
}
