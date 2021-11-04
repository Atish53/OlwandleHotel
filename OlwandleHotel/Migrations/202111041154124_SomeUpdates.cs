namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeUpdates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventBookings",
                c => new
                    {
                        EventBookingId = c.Int(nullable: false, identity: true),
                        EventId = c.Int(nullable: false),
                        CustomerName = c.String(),
                        CustomerSurname = c.String(),
                        Address = c.String(),
                        IdNumber = c.String(),
                        PhoneNumber = c.String(),
                        DateBooked = c.DateTime(nullable: false),
                        TicketNumber = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.EventBookingId)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.EventId)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventBookings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.EventBookings", "EventId", "dbo.Events");
            DropIndex("dbo.EventBookings", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.EventBookings", new[] { "EventId" });
            DropTable("dbo.EventBookings");
        }
    }
}
