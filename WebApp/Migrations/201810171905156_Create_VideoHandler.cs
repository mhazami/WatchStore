namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_VideoHandler : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VideoHandlers",
                c => new
                    {
                        VideoId = c.Guid(nullable: false),
                        Position = c.Byte(nullable: false),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.VideoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VideoHandlers");
        }
    }
}
