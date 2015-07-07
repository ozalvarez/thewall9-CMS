namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlogCategoryNameAsString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BlogCategoryCultures", "BlogCategoryName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BlogCategoryCultures", "BlogCategoryName", c => c.Int(nullable: false));
        }
    }
}
