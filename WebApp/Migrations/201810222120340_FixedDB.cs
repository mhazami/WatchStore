namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BasketOrders",
                c => new
                    {
                        OrderId = c.Guid(nullable: false),
                        BasketId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.BasketId });
            
            AlterColumn("dbo.Baskets", "IsArchive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Baskets", "IsArchive", c => c.Boolean());
            DropTable("dbo.BasketOrders");
        }
    }
}
