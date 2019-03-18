namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Order_And_BasketOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Guid(nullable: false),
                        SaveDate = c.DateTime(nullable: false),
                        IsFinaly = c.Boolean(nullable: false),
                        RefID = c.Long(nullable: false),
                        CurrentPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Orders");
        }
    }
}
