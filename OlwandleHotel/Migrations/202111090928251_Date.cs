namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Date : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "Cost", c => c.Double(nullable: false));
            AddColumn("dbo.RoomBookings", "CheckoutDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.RoomBookings", "CheckinDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoomBookings", "CheckinDate");
            DropColumn("dbo.RoomBookings", "CheckoutDate");
            DropColumn("dbo.Rooms", "Cost");
        }
    }
}
