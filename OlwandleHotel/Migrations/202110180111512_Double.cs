namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Double : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rooms", "FixedCost", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rooms", "FixedCost", c => c.Int(nullable: false));
        }
    }
}
