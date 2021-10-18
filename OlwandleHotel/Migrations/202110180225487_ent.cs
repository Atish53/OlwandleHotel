namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ent : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ReservedBookings", "Name", c => c.String());
            AlterColumn("dbo.ReservedBookings", "LastName", c => c.String());
            AlterColumn("dbo.ReservedBookings", "IDNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ReservedBookings", "IDNumber", c => c.String(nullable: false, maxLength: 13));
            AlterColumn("dbo.ReservedBookings", "LastName", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.ReservedBookings", "Name", c => c.String(nullable: false, maxLength: 80));
        }
    }
}
