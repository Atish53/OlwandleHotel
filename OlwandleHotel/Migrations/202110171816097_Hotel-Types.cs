namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HotelTypes : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Rooms", "RoomTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "RoomTypeId", c => c.Int(nullable: false));
        }
    }
}
