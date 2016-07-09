namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIconUrlToCategories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "IconUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "IconUrl");
        }
    }
}
