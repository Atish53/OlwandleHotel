namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Quotes",
                c => new
                    {
                        QuoteId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmailAddress = c.String(),
                        PhoneNumber = c.String(),
                        NumAdults = c.Int(nullable: false),
                        NumKids = c.Int(nullable: false),
                        DepartureDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                        TourL = c.Int(nullable: false),
                        CruiseL = c.Int(nullable: false),
                        estimatedPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.QuoteId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Quotes");
        }
    }
}
