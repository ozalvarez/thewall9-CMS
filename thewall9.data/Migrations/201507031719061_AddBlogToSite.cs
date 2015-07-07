namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBlogToSite : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sites", "Blog", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sites", "Blog");
        }
    }
}
