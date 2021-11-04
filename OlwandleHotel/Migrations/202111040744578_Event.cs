namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Event : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "EventPicture", c => c.Binary());
            AddColumn("dbo.Events", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "Name");
            DropColumn("dbo.Events", "EventPicture");
        }
    }
}
