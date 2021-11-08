namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roomNumber : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rooms", "RoomNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rooms", "RoomNumber", c => c.Boolean(nullable: false));
        }
    }
}
