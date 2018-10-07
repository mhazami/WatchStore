namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Products : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "IsDeals", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "IsNewSeason", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "IsNewSeason");
            DropColumn("dbo.Products", "IsDeals");
        }
    }
}
