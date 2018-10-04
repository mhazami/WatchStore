namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Count", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "PriceWithOff");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "PriceWithOff", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Products", "Count");
        }
    }
}
