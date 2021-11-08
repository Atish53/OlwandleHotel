namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qr : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReservedBookings", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.RoomBookings", "RoomId", "dbo.Rooms");
            DropIndex("dbo.Rooms", new[] { "RoomId" });
            RenameColumn(table: "dbo.Hotels", name: "RoomId", newName: "Rooms_RoomId");
            DropPrimaryKey("dbo.Rooms");
            AddColumn("dbo.Rooms", "RoomLocation", c => c.String(nullable: false));
            AlterColumn("dbo.Rooms", "RoomId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Rooms", "RoomId");
            CreateIndex("dbo.Hotels", "Rooms_RoomId");
            AddForeignKey("dbo.ReservedBookings", "RoomId", "dbo.Rooms", "RoomId", cascadeDelete: true);
            AddForeignKey("dbo.Hotels", "Rooms_RoomId", "dbo.Rooms", "RoomId");
            AddForeignKey("dbo.RoomBookings", "RoomId", "dbo.Rooms", "RoomId", cascadeDelete: true);
            DropColumn("dbo.Rooms", "HotelId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "HotelId", c => c.String());
            DropForeignKey("dbo.RoomBookings", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Hotels", "Rooms_RoomId", "dbo.Rooms");
            DropForeignKey("dbo.ReservedBookings", "RoomId", "dbo.Rooms");
            DropIndex("dbo.Hotels", new[] { "Rooms_RoomId" });
            DropPrimaryKey("dbo.Rooms");
            AlterColumn("dbo.Rooms", "RoomId", c => c.Int(nullable: false));
            DropColumn("dbo.Rooms", "RoomLocation");
            AddPrimaryKey("dbo.Rooms", "RoomId");
            RenameColumn(table: "dbo.Hotels", name: "Rooms_RoomId", newName: "RoomId");
            CreateIndex("dbo.Rooms", "RoomId");
            AddForeignKey("dbo.RoomBookings", "RoomId", "dbo.Rooms", "RoomId", cascadeDelete: true);
            AddForeignKey("dbo.ReservedBookings", "RoomId", "dbo.Rooms", "RoomId", cascadeDelete: true);
        }
    }
}
