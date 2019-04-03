namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_LangId : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Advertises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileId = c.Guid(nullable: false),
                        Enable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Files", t => t.FileId, cascadeDelete: true)
                .Index(t => t.FileId);
            
            AlterColumn("dbo.AboutUs", "LangId", c => c.String(maxLength: 5));
            AlterColumn("dbo.Baskets", "LangId", c => c.String(maxLength: 5));
            AlterColumn("dbo.Customers", "LangId", c => c.String(maxLength: 5));
            AlterColumn("dbo.Products", "LangId", c => c.String(maxLength: 5));
            AlterColumn("dbo.Products", "OfferType", c => c.Byte());
            AlterColumn("dbo.Categories", "LangId", c => c.String(maxLength: 5));
            AlterColumn("dbo.ExtraImages", "LangId", c => c.String(maxLength: 5));
            AlterColumn("dbo.Cantacts", "LangId", c => c.String(maxLength: 5));
            AlterColumn("dbo.News", "LangId", c => c.String(maxLength: 5));
            AlterColumn("dbo.Sliders", "LangId", c => c.String(maxLength: 5));
            AlterColumn("dbo.Users", "LangId", c => c.String(maxLength: 5));
            AlterColumn("dbo.WallPapers", "LangId", c => c.String(maxLength: 5));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Advertises", "FileId", "dbo.Files");
            DropIndex("dbo.Advertises", new[] { "FileId" });
            AlterColumn("dbo.WallPapers", "LangId", c => c.String(nullable: false, maxLength: 5));
            AlterColumn("dbo.Users", "LangId", c => c.String(nullable: false, maxLength: 5));
            AlterColumn("dbo.Sliders", "LangId", c => c.String(nullable: false, maxLength: 5));
            AlterColumn("dbo.News", "LangId", c => c.String(nullable: false, maxLength: 5));
            AlterColumn("dbo.Cantacts", "LangId", c => c.String(nullable: false, maxLength: 5));
            AlterColumn("dbo.ExtraImages", "LangId", c => c.String(nullable: false, maxLength: 5));
            AlterColumn("dbo.Categories", "LangId", c => c.String(nullable: false, maxLength: 5));
            AlterColumn("dbo.Products", "OfferType", c => c.Byte(nullable: false));
            AlterColumn("dbo.Products", "LangId", c => c.String(nullable: false, maxLength: 5));
            AlterColumn("dbo.Customers", "LangId", c => c.String(nullable: false, maxLength: 5));
            AlterColumn("dbo.Baskets", "LangId", c => c.String(nullable: false, maxLength: 5));
            AlterColumn("dbo.AboutUs", "LangId", c => c.String(nullable: false, maxLength: 5));
            DropTable("dbo.Advertises");
        }
    }
}
