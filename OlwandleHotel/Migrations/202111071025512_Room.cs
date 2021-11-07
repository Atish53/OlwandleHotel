namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Room : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReservedBookings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ReservedBookings", new[] { "ApplicationUser_Id" });
            CreateTable(
                "dbo.RoomBookings",
                c => new
                    {
                        RoomBookingId = c.Int(nullable: false, identity: true),
                        RoomId = c.Int(nullable: false),
                        CustomerName = c.String(),
                        CustomerSurname = c.String(),
                        Address = c.String(),
                        IdNumber = c.String(),
                        PhoneNumber = c.String(),
                        DateBooked = c.DateTime(nullable: false),
                        InvoiceNumber = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RoomBookingId)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.RoomId)
                .Index(t => t.ApplicationUser_Id);
            
            DropColumn("dbo.ReservedBookings", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReservedBookings", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.RoomBookings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.RoomBookings", "RoomId", "dbo.Rooms");
            DropIndex("dbo.RoomBookings", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.RoomBookings", new[] { "RoomId" });
            DropTable("dbo.RoomBookings");
            CreateIndex("dbo.ReservedBookings", "ApplicationUser_Id");
            AddForeignKey("dbo.ReservedBookings", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
