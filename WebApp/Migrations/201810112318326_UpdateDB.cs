namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.News", "FileId", "dbo.Files");
            DropIndex("dbo.News", new[] { "FileId" });
            RenameColumn(table: "dbo.News", name: "FileId", newName: "Image_FileId");
            CreateTable(
                "dbo.WallPapers",
                c => new
                    {
                        WallPaperId = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                        Position = c.Short(nullable: false),
                        FileId = c.Guid(nullable: false),
                        Description = c.String(nullable: false),
                        LangId = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => t.WallPaperId)
                .ForeignKey("dbo.Files", t => t.FileId, cascadeDelete: true)
                .Index(t => t.FileId);
            
            AddColumn("dbo.AboutUs", "LangId", c => c.String(nullable: false, maxLength: 5));
            AddColumn("dbo.Baskets", "LangId", c => c.String(nullable: false, maxLength: 5));
            AddColumn("dbo.Customers", "LangId", c => c.String(nullable: false, maxLength: 5));
            AddColumn("dbo.Products", "LangId", c => c.String(nullable: false, maxLength: 5));
            AddColumn("dbo.Products", "IsBlocked", c => c.Boolean(nullable: false));
            AddColumn("dbo.ExtraImages", "LangId", c => c.String(nullable: false, maxLength: 5));
            AddColumn("dbo.Cantacts", "LangId", c => c.String(nullable: false, maxLength: 5));
            AddColumn("dbo.Categories", "LangId", c => c.String(nullable: false, maxLength: 5));
            AddColumn("dbo.Languages", "Title", c => c.String(nullable: false));
            AddColumn("dbo.News", "Content", c => c.String());
            AddColumn("dbo.News", "ImageId", c => c.Guid(nullable: false));
            AddColumn("dbo.News", "VideoId", c => c.Guid(nullable: false));
            AddColumn("dbo.News", "Video_FileId", c => c.Guid());
            AddColumn("dbo.Sliders", "LangId", c => c.String(nullable: false, maxLength: 5));
            AddColumn("dbo.Users", "LangId", c => c.String(nullable: false, maxLength: 5));
            AlterColumn("dbo.Languages", "LangId", c => c.String(nullable: false, maxLength: 5));
            AlterColumn("dbo.News", "Title", c => c.String());
            AlterColumn("dbo.News", "Image_FileId", c => c.Guid());
            CreateIndex("dbo.News", "Image_FileId");
            CreateIndex("dbo.News", "Video_FileId");
            AddForeignKey("dbo.News", "Video_FileId", "dbo.Files", "FileId");
            AddForeignKey("dbo.News", "Image_FileId", "dbo.Files", "FileId");
            DropColumn("dbo.News", "Position");
            DropColumn("dbo.News", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.News", "Description", c => c.String(nullable: false));
            AddColumn("dbo.News", "Position", c => c.Short(nullable: false));
            DropForeignKey("dbo.News", "Image_FileId", "dbo.Files");
            DropForeignKey("dbo.WallPapers", "FileId", "dbo.Files");
            DropForeignKey("dbo.News", "Video_FileId", "dbo.Files");
            DropIndex("dbo.WallPapers", new[] { "FileId" });
            DropIndex("dbo.News", new[] { "Video_FileId" });
            DropIndex("dbo.News", new[] { "Image_FileId" });
            AlterColumn("dbo.News", "Image_FileId", c => c.Guid(nullable: false));
            AlterColumn("dbo.News", "Title", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Languages", "LangId", c => c.String());
            DropColumn("dbo.Users", "LangId");
            DropColumn("dbo.Sliders", "LangId");
            DropColumn("dbo.News", "Video_FileId");
            DropColumn("dbo.News", "VideoId");
            DropColumn("dbo.News", "ImageId");
            DropColumn("dbo.News", "Content");
            DropColumn("dbo.Languages", "Title");
            DropColumn("dbo.Categories", "LangId");
            DropColumn("dbo.Cantacts", "LangId");
            DropColumn("dbo.ExtraImages", "LangId");
            DropColumn("dbo.Products", "IsBlocked");
            DropColumn("dbo.Products", "LangId");
            DropColumn("dbo.Customers", "LangId");
            DropColumn("dbo.Baskets", "LangId");
            DropColumn("dbo.AboutUs", "LangId");
            DropTable("dbo.WallPapers");
            RenameColumn(table: "dbo.News", name: "Image_FileId", newName: "FileId");
            CreateIndex("dbo.News", "FileId");
            AddForeignKey("dbo.News", "FileId", "dbo.Files", "FileId", cascadeDelete: true);
        }
    }
}
