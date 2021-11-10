namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Date1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Flights", "DateFlight", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Flights", "DateReturn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Flights", "DateReturn", c => c.String());
            AlterColumn("dbo.Flights", "DateFlight", c => c.String());
        }
    }
}
