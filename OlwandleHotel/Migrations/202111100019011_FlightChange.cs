namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FlightChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FlightBookings", "FlightId", "dbo.Flights");
            DropForeignKey("dbo.FlightBookings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.FlightBookings", new[] { "FlightId" });
            DropIndex("dbo.FlightBookings", new[] { "ApplicationUser_Id" });
            AddColumn("dbo.Flights", "CustomerName", c => c.String());
            AddColumn("dbo.Flights", "CustomerSurname", c => c.String());
            AddColumn("dbo.Flights", "Address", c => c.String());
            AddColumn("dbo.Flights", "IdNumber", c => c.String());
            AddColumn("dbo.Flights", "PhoneNumber", c => c.String());
            AddColumn("dbo.Flights", "DateBooked", c => c.String());
            AddColumn("dbo.Flights", "BoardDateAndTime", c => c.String());
            AddColumn("dbo.Flights", "TicketNumber", c => c.String());
            AddColumn("dbo.Flights", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Flights", "ApplicationUser_Id");
            AddForeignKey("dbo.Flights", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            DropTable("dbo.FlightBookings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FlightBookings",
                c => new
                    {
                        FlightBookingId = c.Int(nullable: false, identity: true),
                        FlightId = c.Int(nullable: false),
                        CustomerName = c.String(),
                        CustomerSurname = c.String(),
                        Address = c.String(),
                        IdNumber = c.String(),
                        PhoneNumber = c.String(),
                        DateBooked = c.String(),
                        BoardDateAndTime = c.String(),
                        TicketNumber = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.FlightBookingId);
            
            DropForeignKey("dbo.Flights", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Flights", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Flights", "ApplicationUser_Id");
            DropColumn("dbo.Flights", "TicketNumber");
            DropColumn("dbo.Flights", "BoardDateAndTime");
            DropColumn("dbo.Flights", "DateBooked");
            DropColumn("dbo.Flights", "PhoneNumber");
            DropColumn("dbo.Flights", "IdNumber");
            DropColumn("dbo.Flights", "Address");
            DropColumn("dbo.Flights", "CustomerSurname");
            DropColumn("dbo.Flights", "CustomerName");
            CreateIndex("dbo.FlightBookings", "ApplicationUser_Id");
            CreateIndex("dbo.FlightBookings", "FlightId");
            AddForeignKey("dbo.FlightBookings", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.FlightBookings", "FlightId", "dbo.Flights", "FlightId", cascadeDelete: true);
        }
    }
}
