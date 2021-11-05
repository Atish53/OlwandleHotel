namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventRefund : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventRefunds",
                c => new
                    {
                        RefundId = c.Int(nullable: false, identity: true),
                        EventBookingId = c.Int(nullable: false),
                        RefundReason = c.String(),
                        RefundStatus = c.String(),
                        isRefundActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RefundId)
                .ForeignKey("dbo.EventBookings", t => t.EventBookingId, cascadeDelete: true)
                .Index(t => t.EventBookingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventRefunds", "EventBookingId", "dbo.EventBookings");
            DropIndex("dbo.EventRefunds", new[] { "EventBookingId" });
            DropTable("dbo.EventRefunds");
        }
    }
}
