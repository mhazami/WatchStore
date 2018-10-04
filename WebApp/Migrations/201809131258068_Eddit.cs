namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Eddit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ExtraImagesProducts", "FileId", "dbo.Files");
            DropIndex("dbo.ExtraImagesProducts", new[] { "FileId" });
            CreateTable(
                "dbo.ExtraImages",
                c => new
                    {
                        ExtraImagesId = c.Guid(nullable: false),
                        FileId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ExtraImagesId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            DropTable("dbo.ExtraImagesProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ExtraImagesProducts",
                c => new
                    {
                        ExtraImagesProductId = c.Guid(nullable: false),
                        FileId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ExtraImagesProductId);
            
            DropForeignKey("dbo.ExtraImages", "ProductId", "dbo.Products");
            DropIndex("dbo.ExtraImages", new[] { "ProductId" });
            DropTable("dbo.ExtraImages");
            CreateIndex("dbo.ExtraImagesProducts", "FileId");
            AddForeignKey("dbo.ExtraImagesProducts", "FileId", "dbo.Files", "FileId", cascadeDelete: true);
        }
    }
}
