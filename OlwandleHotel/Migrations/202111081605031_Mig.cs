namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "RoomAddress", c => c.String(nullable: false));
            AddColumn("dbo.Rooms", "RoomNumber", c => c.Boolean(nullable: false));
            AddColumn("dbo.Rooms", "Room_RoomId", c => c.Int());
            CreateIndex("dbo.Rooms", "Room_RoomId");
            AddForeignKey("dbo.Rooms", "Room_RoomId", "dbo.Rooms", "RoomId");
            DropColumn("dbo.Rooms", "RoomStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "RoomStatus", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Rooms", "Room_RoomId", "dbo.Rooms");
            DropIndex("dbo.Rooms", new[] { "Room_RoomId" });
            DropColumn("dbo.Rooms", "Room_RoomId");
            DropColumn("dbo.Rooms", "RoomNumber");
            DropColumn("dbo.Rooms", "RoomAddress");
        }
    }
}
