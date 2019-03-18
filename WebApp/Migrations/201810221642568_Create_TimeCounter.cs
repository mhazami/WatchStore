namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_TimeCounter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TimeCounters",
                c => new
                    {
                        TimeCounterId = c.Guid(nullable: false),
                        TimeRange = c.String(),
                    })
                .PrimaryKey(t => t.TimeCounterId);
            
            AddColumn("dbo.Products", "OfferType", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "OfferType");
            DropTable("dbo.TimeCounters");
        }
    }
}
