namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLockToContentProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContentProperties", "Lock", c => c.Boolean(nullable: false,defaultValue:false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContentProperties", "Lock");
        }
    }
}
