namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FinalizedBookingDetails", "FinalCost", c => c.Int(nullable: false));
            AddColumn("dbo.FinalizedBookingDetails", "FinalizedBooking_BookingID", c => c.Int());
            AddColumn("dbo.FinalizedBookingDetails", "ReservedBooking_ReservedBookingId", c => c.Int());
            AddColumn("dbo.FinalizedBookings", "Name", c => c.String(nullable: false, maxLength: 80));
            AddColumn("dbo.FinalizedBookings", "LastName", c => c.String(nullable: false, maxLength: 80));
            AddColumn("dbo.FinalizedBookings", "IDNumber", c => c.String(nullable: false, maxLength: 13));
            AddColumn("dbo.FinalizedBookings", "ReservedBooking_ReservedBookingId", c => c.Int());
            AddColumn("dbo.FinalizedBookings", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.ReservedBookings", "Name", c => c.String(nullable: false, maxLength: 80));
            AddColumn("dbo.ReservedBookings", "LastName", c => c.String(nullable: false, maxLength: 80));
            AddColumn("dbo.ReservedBookings", "IDNumber", c => c.String(nullable: false, maxLength: 13));
            AddColumn("dbo.ReservedBookings", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "IDNumber", c => c.String());
            CreateIndex("dbo.FinalizedBookingDetails", "FinalizedBooking_BookingID");
            CreateIndex("dbo.FinalizedBookingDetails", "ReservedBooking_ReservedBookingId");
            CreateIndex("dbo.FinalizedBookings", "ReservedBooking_ReservedBookingId");
            CreateIndex("dbo.FinalizedBookings", "ApplicationUser_Id");
            CreateIndex("dbo.ReservedBookings", "ApplicationUser_Id");
            AddForeignKey("dbo.FinalizedBookingDetails", "FinalizedBooking_BookingID", "dbo.FinalizedBookings", "BookingID");
            AddForeignKey("dbo.FinalizedBookings", "ReservedBooking_ReservedBookingId", "dbo.ReservedBookings", "ReservedBookingId");
            AddForeignKey("dbo.FinalizedBookingDetails", "ReservedBooking_ReservedBookingId", "dbo.ReservedBookings", "ReservedBookingId");
            AddForeignKey("dbo.FinalizedBookings", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ReservedBookings", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReservedBookings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FinalizedBookings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FinalizedBookingDetails", "ReservedBooking_ReservedBookingId", "dbo.ReservedBookings");
            DropForeignKey("dbo.FinalizedBookings", "ReservedBooking_ReservedBookingId", "dbo.ReservedBookings");
            DropForeignKey("dbo.FinalizedBookingDetails", "FinalizedBooking_BookingID", "dbo.FinalizedBookings");
            DropIndex("dbo.ReservedBookings", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.FinalizedBookings", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.FinalizedBookings", new[] { "ReservedBooking_ReservedBookingId" });
            DropIndex("dbo.FinalizedBookingDetails", new[] { "ReservedBooking_ReservedBookingId" });
            DropIndex("dbo.FinalizedBookingDetails", new[] { "FinalizedBooking_BookingID" });
            DropColumn("dbo.AspNetUsers", "IDNumber");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.ReservedBookings", "ApplicationUser_Id");
            DropColumn("dbo.ReservedBookings", "IDNumber");
            DropColumn("dbo.ReservedBookings", "LastName");
            DropColumn("dbo.ReservedBookings", "Name");
            DropColumn("dbo.FinalizedBookings", "ApplicationUser_Id");
            DropColumn("dbo.FinalizedBookings", "ReservedBooking_ReservedBookingId");
            DropColumn("dbo.FinalizedBookings", "IDNumber");
            DropColumn("dbo.FinalizedBookings", "LastName");
            DropColumn("dbo.FinalizedBookings", "Name");
            DropColumn("dbo.FinalizedBookingDetails", "ReservedBooking_ReservedBookingId");
            DropColumn("dbo.FinalizedBookingDetails", "FinalizedBooking_BookingID");
            DropColumn("dbo.FinalizedBookingDetails", "FinalCost");
        }
    }
}
