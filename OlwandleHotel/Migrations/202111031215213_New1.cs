namespace OlwandleHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New1 : DbMigration
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
                "dbo.Hotels",
                c => new
                    {
                        HotelId = c.Int(nullable: false, identity: true),
                        HotelPicture = c.Binary(),
                        HotelLocation = c.String(),
                        HotelName = c.String(),
                        HotelDescription = c.String(),
                    })
                .PrimaryKey(t => t.HotelId);
            
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
            
            AddColumn("dbo.Rooms", "HotelId", c => c.String(nullable: false));
            AddColumn("dbo.Rooms", "RoomPicture", c => c.Binary());
            AddColumn("dbo.Rooms", "Hotel_HotelId", c => c.Int());
            CreateIndex("dbo.Rooms", "Hotel_HotelId");
            AddForeignKey("dbo.Rooms", "Hotel_HotelId", "dbo.Hotels", "HotelId");
            DropColumn("dbo.Rooms", "RoomLocation");
            DropColumn("dbo.Rooms", "Picture");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "Picture", c => c.Binary());
            AddColumn("dbo.Rooms", "RoomLocation", c => c.String(nullable: false));
            DropForeignKey("dbo.SaleDetails", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.SaleDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Rooms", "Hotel_HotelId", "dbo.Hotels");
            DropForeignKey("dbo.Carts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductCategory_CatergoryId", "dbo.ProductCategories");
            DropIndex("dbo.SaleDetails", new[] { "ProductId" });
            DropIndex("dbo.SaleDetails", new[] { "SaleId" });
            DropIndex("dbo.Rooms", new[] { "Hotel_HotelId" });
            DropIndex("dbo.Products", new[] { "ProductCategory_CatergoryId" });
            DropIndex("dbo.Carts", new[] { "ProductId" });
            DropColumn("dbo.Rooms", "Hotel_HotelId");
            DropColumn("dbo.Rooms", "RoomPicture");
            DropColumn("dbo.Rooms", "HotelId");
            DropTable("dbo.Sales");
            DropTable("dbo.SaleDetails");
            DropTable("dbo.Hotels");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Products");
            DropTable("dbo.Carts");
        }
    }
}
