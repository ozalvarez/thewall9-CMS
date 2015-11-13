namespace thewall9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDYoutubeChannel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cultures", "YoutubeChannel", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cultures", "YoutubeChannel");
        }
    }
}
