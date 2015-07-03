namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePublishedOnBlogPost : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BlogPosts", "Published");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BlogPosts", "Published", c => c.Boolean(nullable: false));
        }
    }
}
