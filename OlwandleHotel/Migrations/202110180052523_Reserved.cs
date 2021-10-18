namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reserved : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReservedBookings", "ReservedCost", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReservedBookings", "ReservedCost");
        }
    }
}
