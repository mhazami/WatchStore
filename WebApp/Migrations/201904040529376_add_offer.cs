namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_offer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerOffers",
                c => new
                    {
                        CustomerId = c.Guid(nullable: false),
                        OfferCardtId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CustomerId, t.OfferCardtId });
            
            CreateTable(
                "dbo.OfferCards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OfferType = c.Byte(nullable: false),
                        OfferCode = c.String(),
                        OfferAmount = c.Int(nullable: false),
                        Enable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OfferCards");
            DropTable("dbo.CustomerOffers");
        }
    }
}
