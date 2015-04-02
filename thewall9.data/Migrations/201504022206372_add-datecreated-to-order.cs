namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddatecreatedtoorder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "DateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "DateCreated");
        }
    }
}
