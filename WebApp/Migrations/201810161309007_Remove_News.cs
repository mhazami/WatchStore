namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_News : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.News", "Image_FileId", "dbo.Files");
            DropForeignKey("dbo.News", "Video_FileId", "dbo.Files");
            DropIndex("dbo.News", new[] { "Image_FileId" });
            DropIndex("dbo.News", new[] { "Video_FileId" });
            DropTable("dbo.News");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewsId = c.Guid(nullable: false),
                        Title = c.String(),
                        Content = c.String(),
                        ImageId = c.Guid(nullable: false),
                        VideoId = c.Guid(nullable: false),
                        LangId = c.String(nullable: false, maxLength: 5),
                        Image_FileId = c.Guid(),
                        Video_FileId = c.Guid(),
                    })
                .PrimaryKey(t => t.NewsId);
            
            CreateIndex("dbo.News", "Video_FileId");
            CreateIndex("dbo.News", "Image_FileId");
            AddForeignKey("dbo.News", "Video_FileId", "dbo.Files", "FileId");
            AddForeignKey("dbo.News", "Image_FileId", "dbo.Files", "FileId");
        }
    }
}
