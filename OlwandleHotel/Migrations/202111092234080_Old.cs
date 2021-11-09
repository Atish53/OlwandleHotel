namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Old : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CruiseBookings",
                c => new
                    {
                        CruiseBookingId = c.Int(nullable: false, identity: true),
                        CruiseId = c.Int(nullable: false),
                        CustomerName = c.String(),
                        CustomerSurname = c.String(),
                        Address = c.String(),
                        IdNumber = c.String(),
                        PhoneNumber = c.String(),
                        DateBooked = c.String(),
                        AttendDateAndTime = c.String(),
                        TicketNumber = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CruiseBookingId)
                .ForeignKey("dbo.Cruises", t => t.CruiseId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.CruiseId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Cruises",
                c => new
                    {
                        CruiseId = c.Int(nullable: false, identity: true),
                        CruiseTitle = c.String(),
                        CruisePicture = c.Binary(),
                        CruiseName = c.String(),
                        CruiseDestinations = c.String(),
                        CruiseLocation = c.String(),
                        CruisePrice = c.Double(nullable: false),
                        CruiseTicketsRemaining = c.Int(nullable: false),
                        isActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CruiseId);
            
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
                .PrimaryKey(t => t.FlightBookingId)
                .ForeignKey("dbo.Flights", t => t.FlightId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.FlightId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Flights",
                c => new
                    {
                        FlightId = c.Int(nullable: false, identity: true),
                        FlightL = c.Int(nullable: false),
                        DestinationL = c.Int(nullable: false),
                        DateFlight = c.String(),
                        DateReturn = c.String(),
                        returnTicket = c.Boolean(nullable: false),
                        TotalCost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.FlightId);
            
            CreateTable(
                "dbo.TourBookings",
                c => new
                    {
                        TourBookingId = c.Int(nullable: false, identity: true),
                        TourId = c.Int(nullable: false),
                        CustomerName = c.String(),
                        CustomerSurname = c.String(),
                        Address = c.String(),
                        IdNumber = c.String(),
                        PhoneNumber = c.String(),
                        DateBooked = c.String(),
                        AttendDateAndTime = c.String(),
                        TicketNumber = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TourBookingId)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.TourId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Tours",
                c => new
                    {
                        TourId = c.Int(nullable: false, identity: true),
                        TourTitle = c.String(),
                        TourPicture = c.Binary(),
                        TourName = c.String(),
                        TourDestinations = c.String(),
                        TourLocation = c.String(),
                        TourPrice = c.Double(nullable: false),
                        TourTicketsRemaining = c.Int(nullable: false),
                        isActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TourId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TourBookings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TourBookings", "TourId", "dbo.Tours");
            DropForeignKey("dbo.FlightBookings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FlightBookings", "FlightId", "dbo.Flights");
            DropForeignKey("dbo.CruiseBookings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CruiseBookings", "CruiseId", "dbo.Cruises");
            DropIndex("dbo.TourBookings", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TourBookings", new[] { "TourId" });
            DropIndex("dbo.FlightBookings", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.FlightBookings", new[] { "FlightId" });
            DropIndex("dbo.CruiseBookings", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.CruiseBookings", new[] { "CruiseId" });
            DropTable("dbo.Tours");
            DropTable("dbo.TourBookings");
            DropTable("dbo.Flights");
            DropTable("dbo.FlightBookings");
            DropTable("dbo.Cruises");
            DropTable("dbo.CruiseBookings");
        }
    }
}
