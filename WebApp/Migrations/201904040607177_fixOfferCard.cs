namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixOfferCard : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CustomerOffers");
            AddColumn("dbo.CustomerOffers", "OfferCardId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.CustomerOffers", new[] { "CustomerId", "OfferCardId" });
            DropColumn("dbo.CustomerOffers", "OfferCardtId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerOffers", "OfferCardtId", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.CustomerOffers");
            DropColumn("dbo.CustomerOffers", "OfferCardId");
            AddPrimaryKey("dbo.CustomerOffers", new[] { "CustomerId", "OfferCardtId" });
        }
    }
}
