namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Room : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReservedBookings", "RoomId", c => c.Int(nullable: false));
            CreateIndex("dbo.ReservedBookings", "RoomId");
            AddForeignKey("dbo.ReservedBookings", "RoomId", "dbo.Rooms", "RoomId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReservedBookings", "RoomId", "dbo.Rooms");
            DropIndex("dbo.ReservedBookings", new[] { "RoomId" });
            DropColumn("dbo.ReservedBookings", "RoomId");
        }
    }
}
