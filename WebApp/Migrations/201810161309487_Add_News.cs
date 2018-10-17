namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_News : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewsId = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        ImageId = c.Guid(nullable: false),
                        VideoId = c.Guid(nullable: false),
                        LangId = c.String(nullable: false, maxLength: 5),
                        Image_FileId = c.Guid(),
                        Video_FileId = c.Guid(),
                    })
                .PrimaryKey(t => t.NewsId)
                .ForeignKey("dbo.Files", t => t.Image_FileId)
                .ForeignKey("dbo.Files", t => t.Video_FileId)
                .Index(t => t.Image_FileId)
                .Index(t => t.Video_FileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "Video_FileId", "dbo.Files");
            DropForeignKey("dbo.News", "Image_FileId", "dbo.Files");
            DropIndex("dbo.News", new[] { "Video_FileId" });
            DropIndex("dbo.News", new[] { "Image_FileId" });
            DropTable("dbo.News");
        }
    }
}
