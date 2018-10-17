namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_FileId_From_News : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.News", "Image_FileId", "dbo.Files");
            DropForeignKey("dbo.News", "Video_FileId", "dbo.Files");
            DropIndex("dbo.News", new[] { "Image_FileId" });
            DropIndex("dbo.News", new[] { "Video_FileId" });
            DropColumn("dbo.News", "Image_FileId");
            DropColumn("dbo.News", "Video_FileId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.News", "Video_FileId", c => c.Guid());
            AddColumn("dbo.News", "Image_FileId", c => c.Guid());
            CreateIndex("dbo.News", "Video_FileId");
            CreateIndex("dbo.News", "Image_FileId");
            AddForeignKey("dbo.News", "Video_FileId", "dbo.Files", "FileId");
            AddForeignKey("dbo.News", "Image_FileId", "dbo.Files", "FileId");
        }
    }
}
