namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventRefunds", "EventBookingId", "dbo.EventBookings");
            DropIndex("dbo.EventRefunds", new[] { "EventBookingId" });
            RenameColumn(table: "dbo.EventRefunds", name: "EventBookingId", newName: "EventBooking_EventBookingId");
            AddColumn("dbo.EventRefunds", "TicketNumber", c => c.String());
            AlterColumn("dbo.EventRefunds", "EventBooking_EventBookingId", c => c.Int());
            CreateIndex("dbo.EventRefunds", "EventBooking_EventBookingId");
            AddForeignKey("dbo.EventRefunds", "EventBooking_EventBookingId", "dbo.EventBookings", "EventBookingId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventRefunds", "EventBooking_EventBookingId", "dbo.EventBookings");
            DropIndex("dbo.EventRefunds", new[] { "EventBooking_EventBookingId" });
            AlterColumn("dbo.EventRefunds", "EventBooking_EventBookingId", c => c.Int(nullable: false));
            DropColumn("dbo.EventRefunds", "TicketNumber");
            RenameColumn(table: "dbo.EventRefunds", name: "EventBooking_EventBookingId", newName: "EventBookingId");
            CreateIndex("dbo.EventRefunds", "EventBookingId");
            AddForeignKey("dbo.EventRefunds", "EventBookingId", "dbo.EventBookings", "EventBookingId", cascadeDelete: true);
        }
    }
}
