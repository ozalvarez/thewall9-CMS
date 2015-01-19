namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContentCultureAddHint : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContentPropertyCultures", "Hint", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContentPropertyCultures", "Hint");
        }
    }
}
