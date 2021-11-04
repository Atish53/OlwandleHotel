namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewEvents : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "isActive", c => c.Boolean(nullable: false));
            DropColumn("dbo.Events", "End");
            DropColumn("dbo.Events", "IsFullDay");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "IsFullDay", c => c.Boolean(nullable: false));
            AddColumn("dbo.Events", "End", c => c.DateTime(nullable: false));
            DropColumn("dbo.Events", "isActive");
        }
    }
}
