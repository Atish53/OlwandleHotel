namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeRoomModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rooms", "Picture", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rooms", "Picture", c => c.Binary(nullable: false));
        }
    }
}
