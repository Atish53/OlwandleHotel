namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tickets : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.Events", "TicketsRemaining", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "TicketsRemaining");
            DropColumn("dbo.Events", "Price");
        }
    }
}
