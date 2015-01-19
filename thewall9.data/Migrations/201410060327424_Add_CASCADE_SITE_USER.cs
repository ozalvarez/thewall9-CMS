namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Add_CASCADE_SITE_USER : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sites", "UserIDOwner", "dbo.AspNetUsers");
            AddForeignKey("dbo.Sites", "UserIDOwner", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Sites", "UserIDOwner", "dbo.AspNetUsers");
            AddForeignKey("dbo.Sites", "UserIDOwner", "dbo.AspNetUsers", "Id");
        }
    }
}
