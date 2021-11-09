namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class str : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RoomBookings", "CheckoutDate", c => c.String());
            AlterColumn("dbo.RoomBookings", "CheckinDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RoomBookings", "CheckinDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RoomBookings", "CheckoutDate", c => c.DateTime(nullable: false));
        }
    }
}
