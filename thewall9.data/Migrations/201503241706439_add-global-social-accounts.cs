namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addglobalsocialaccounts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cultures", "Twitter", c => c.String());
            AddColumn("dbo.Cultures", "Facebook", c => c.String());
            AddColumn("dbo.Cultures", "GPlus", c => c.String());
            AddColumn("dbo.Cultures", "Tumblr", c => c.String());
            AddColumn("dbo.Cultures", "Instagram", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cultures", "Instagram");
            DropColumn("dbo.Cultures", "Tumblr");
            DropColumn("dbo.Cultures", "GPlus");
            DropColumn("dbo.Cultures", "Facebook");
            DropColumn("dbo.Cultures", "Twitter");
        }
    }
}
