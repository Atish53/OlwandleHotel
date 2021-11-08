namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class q : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CartId = c.String(),
                        ProductId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductPicture = c.Binary(),
                        ProductName = c.String(),
                        ProductStock = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductCategory_CatergoryId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategory_CatergoryId)
                .Index(t => t.ProductCategory_CatergoryId);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        CatergoryId = c.Int(nullable: false, identity: true),
                        CategoryPicture = c.Binary(),
                        CategoryName = c.String(),
                        CategoryDescription = c.String(),
                    })
                .PrimaryKey(t => t.CatergoryId);
            
            CreateTable(
                "dbo.EventBookings",
                c => new
                    {
                        EventBookingId = c.Int(nullable: false, identity: true),
                        EventId = c.Int(nullable: false),
                        CustomerName = c.String(),
                        CustomerSurname = c.String(),
                        Address = c.String(),
                        IdNumber = c.String(),
                        PhoneNumber = c.String(),
                        DateBooked = c.DateTime(nullable: false),
                        TicketNumber = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.EventBookingId)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.EventId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        EventPicture = c.Binary(),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Location = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                        TicketsRemaining = c.Int(nullable: false),
                        isActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EventId);
            
            CreateTable(
                "dbo.EventRefunds",
                c => new
                    {
                        RefundId = c.Int(nullable: false, identity: true),
                        TicketNumber = c.String(),
                        RefundReason = c.String(),
                        RefundStatus = c.String(),
                        isRefundActive = c.Boolean(nullable: false),
                        EventBooking_EventBookingId = c.Int(),
                    })
                .PrimaryKey(t => t.RefundId)
                .ForeignKey("dbo.EventBookings", t => t.EventBooking_EventBookingId)
                .Index(t => t.EventBooking_EventBookingId);
            
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        HotelId = c.Int(nullable: false, identity: true),
                        HotelPicture = c.Binary(),
                        HotelLocation = c.String(),
                        HotelName = c.String(),
                        HotelDescription = c.String(),
                        Rooms_RoomId = c.Int(),
                    })
                .PrimaryKey(t => t.HotelId)
                .ForeignKey("dbo.Rooms", t => t.Rooms_RoomId)
                .Index(t => t.Rooms_RoomId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        RoomPicture = c.Binary(),
                        RoomLocation = c.String(nullable: false),
                        RoomStatus = c.Boolean(nullable: false),
                        MaxOccupants = c.Int(nullable: false),
                        NumBeds = c.Int(nullable: false),
                        NumBaths = c.Int(nullable: false),
                        NumLivingRooms = c.Int(nullable: false),
                        RoomsAvailable = c.Int(nullable: false),
                        isActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RoomId);
            
            CreateTable(
                "dbo.ReservedBookings",
                c => new
                    {
                        ReservedBookingId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LastName = c.String(),
                        IDNumber = c.String(),
                        RoomId = c.Int(nullable: false),
                        ReservedCost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ReservedBookingId)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.RoomBookings",
                c => new
                    {
                        RoomBookingId = c.Int(nullable: false, identity: true),
                        RoomId = c.Int(nullable: false),
                        CustomerName = c.String(),
                        CustomerSurname = c.String(),
                        Address = c.String(),
                        IdNumber = c.String(),
                        PhoneNumber = c.String(),
                        DateBooked = c.DateTime(nullable: false),
                        InvoiceNumber = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RoomBookingId)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.RoomId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.SaleDetails",
                c => new
                    {
                        SaleDetailId = c.Int(nullable: false, identity: true),
                        SaleId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.SaleDetailId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Sales", t => t.SaleId, cascadeDelete: true)
                .Index(t => t.SaleId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        SaleDate = c.DateTime(nullable: false),
                        Email = c.String(),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.SaleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        IdNumber = c.String(),
                        Address = c.String(),
                        Points = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomBookings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.EventBookings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SaleDetails", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.SaleDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.RoomBookings", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Hotels", "Rooms_RoomId", "dbo.Rooms");
            DropForeignKey("dbo.ReservedBookings", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.EventRefunds", "EventBooking_EventBookingId", "dbo.EventBookings");
            DropForeignKey("dbo.EventBookings", "EventId", "dbo.Events");
            DropForeignKey("dbo.Carts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductCategory_CatergoryId", "dbo.ProductCategories");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.SaleDetails", new[] { "ProductId" });
            DropIndex("dbo.SaleDetails", new[] { "SaleId" });
            DropIndex("dbo.RoomBookings", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.RoomBookings", new[] { "RoomId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ReservedBookings", new[] { "RoomId" });
            DropIndex("dbo.Hotels", new[] { "Rooms_RoomId" });
            DropIndex("dbo.EventRefunds", new[] { "EventBooking_EventBookingId" });
            DropIndex("dbo.EventBookings", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.EventBookings", new[] { "EventId" });
            DropIndex("dbo.Products", new[] { "ProductCategory_CatergoryId" });
            DropIndex("dbo.Carts", new[] { "ProductId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Sales");
            DropTable("dbo.SaleDetails");
            DropTable("dbo.RoomBookings");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ReservedBookings");
            DropTable("dbo.Rooms");
            DropTable("dbo.Hotels");
            DropTable("dbo.EventRefunds");
            DropTable("dbo.Events");
            DropTable("dbo.EventBookings");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Products");
            DropTable("dbo.Carts");
        }
    }
}
