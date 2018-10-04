namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_DB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AboutUs",
                c => new
                    {
                        AboutUsId = c.Guid(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.AboutUsId);
            
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        BasketId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                        SaveDate = c.DateTime(nullable: false),
                        Status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.BasketId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        UserName = c.String(nullable: false, maxLength: 50),
                        PassWord = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(nullable: false, maxLength: 11),
                        Address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Guid(nullable: false),
                        ProductName = c.String(nullable: false, maxLength: 100),
                        FileId = c.Guid(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Off = c.Int(nullable: false),
                        PriceWithOff = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        CategoryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Files", t => t.FileId, cascadeDelete: true)
                .Index(t => t.FileId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Guid(nullable: false),
                        CategoryName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Guid(nullable: false),
                        Context = c.Binary(),
                        ContextType = c.String(nullable: false, maxLength: 10),
                        Title = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.FileId);
            
            CreateTable(
                "dbo.Cantacts",
                c => new
                    {
                        ContactId = c.Guid(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 150),
                        Phone = c.String(maxLength: 11),
                        Subject = c.String(nullable: false, maxLength: 50),
                        Message = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ContactId);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewsId = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                        Position = c.Short(nullable: false),
                        NewsImage = c.String(maxLength: 100),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.NewsId);
            
            CreateTable(
                "dbo.Sliders",
                c => new
                    {
                        SliderId = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        FileId = c.Guid(nullable: false),
                        IsMainSlider = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SliderId)
                .ForeignKey("dbo.Files", t => t.FileId, cascadeDelete: true)
                .Index(t => t.FileId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        UserName = c.String(nullable: false, maxLength: 50),
                        PassWord = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sliders", "FileId", "dbo.Files");
            DropForeignKey("dbo.Baskets", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "FileId", "dbo.Files");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Baskets", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Sliders", new[] { "FileId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Products", new[] { "FileId" });
            DropIndex("dbo.Baskets", new[] { "CustomerId" });
            DropIndex("dbo.Baskets", new[] { "ProductId" });
            DropTable("dbo.Users");
            DropTable("dbo.Sliders");
            DropTable("dbo.News");
            DropTable("dbo.Cantacts");
            DropTable("dbo.Files");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Customers");
            DropTable("dbo.Baskets");
            DropTable("dbo.AboutUs");
        }
    }
}
