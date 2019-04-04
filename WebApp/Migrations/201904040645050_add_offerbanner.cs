namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_offerbanner : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OfferBanners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileId = c.Guid(nullable: false),
                        Title = c.String(),
                        Timer = c.String(),
                        Enable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Files", t => t.FileId, cascadeDelete: true)
                .Index(t => t.FileId);
            
            AddColumn("dbo.Orders", "OfferCardId", c => c.Int(nullable: false));
            DropTable("dbo.TimeCounters");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TimeCounters",
                c => new
                    {
                        TimeCounterId = c.Guid(nullable: false),
                        TimeRange = c.String(),
                    })
                .PrimaryKey(t => t.TimeCounterId);
            
            DropForeignKey("dbo.OfferBanners", "FileId", "dbo.Files");
            DropIndex("dbo.OfferBanners", new[] { "FileId" });
            DropColumn("dbo.Orders", "OfferCardId");
            DropTable("dbo.OfferBanners");
        }
    }
}
