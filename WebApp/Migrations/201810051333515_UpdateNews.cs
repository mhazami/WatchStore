namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "FileId", c => c.Guid(nullable: false));
            CreateIndex("dbo.News", "FileId");
            AddForeignKey("dbo.News", "FileId", "dbo.Files", "FileId", cascadeDelete: true);
            DropColumn("dbo.News", "NewsImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.News", "NewsImage", c => c.String(maxLength: 100));
            DropForeignKey("dbo.News", "FileId", "dbo.Files");
            DropIndex("dbo.News", new[] { "FileId" });
            DropColumn("dbo.News", "FileId");
        }
    }
}
