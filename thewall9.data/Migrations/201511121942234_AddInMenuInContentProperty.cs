namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInMenuInContentProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContentProperties", "InMenu", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContentProperties", "InMenu");
        }
    }
}
