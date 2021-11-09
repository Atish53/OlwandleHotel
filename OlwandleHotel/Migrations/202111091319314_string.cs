namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _string : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RoomBookings", "DateBooked", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RoomBookings", "DateBooked", c => c.DateTime(nullable: false));
        }
    }
}
