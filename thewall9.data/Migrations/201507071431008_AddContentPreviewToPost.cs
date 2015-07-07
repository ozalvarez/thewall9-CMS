namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContentPreviewToPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogPostCultures", "ContentPreview", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BlogPostCultures", "ContentPreview");
        }
    }
}
