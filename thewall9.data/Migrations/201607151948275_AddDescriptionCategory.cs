namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescriptionCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CategoryCultures", "CategoryDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CategoryCultures", "CategoryDescription");
        }
    }
}
