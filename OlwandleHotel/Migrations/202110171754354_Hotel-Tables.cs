namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HotelTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FinalizedBookingDetails",
                c => new
                    {
                        FinalizedBookingDetailId = c.Int(nullable: false, identity: true),
                        Room_RoomId = c.Int(),
                    })
                .PrimaryKey(t => t.FinalizedBookingDetailId)
                .ForeignKey("dbo.Rooms", t => t.Room_RoomId)
                .Index(t => t.Room_RoomId);
            
            CreateTable(
                "dbo.FinalizedBookings",
                c => new
                    {
                        BookingID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.BookingID);
            
            CreateTable(
                "dbo.ReservedBookings",
                c => new
                    {
                        ReservedBookingId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ReservedBookingId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        RoomLocation = c.String(nullable: false),
                        RoomTypeId = c.Int(nullable: false),
                        Picture = c.Binary(nullable: false),
                        RoomStatus = c.Boolean(nullable: false),
                        MaxOccupants = c.Int(nullable: false),
                        NumBeds = c.Int(nullable: false),
                        NumBaths = c.Int(nullable: false),
                        NumLivingRooms = c.Int(nullable: false),
                        FixedCost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoomId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FinalizedBookingDetails", "Room_RoomId", "dbo.Rooms");
            DropIndex("dbo.FinalizedBookingDetails", new[] { "Room_RoomId" });
            DropTable("dbo.Rooms");
            DropTable("dbo.ReservedBookings");
            DropTable("dbo.FinalizedBookings");
            DropTable("dbo.FinalizedBookingDetails");
        }
    }
}
