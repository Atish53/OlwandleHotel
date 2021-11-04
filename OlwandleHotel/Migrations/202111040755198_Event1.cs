namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Event1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Events", "Start");
            DropColumn("dbo.Events", "ThemeColor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "ThemeColor", c => c.String(nullable: false));
            AddColumn("dbo.Events", "Start", c => c.DateTime(nullable: false));
        }
    }
}
