namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_IsLuxuty_To_Product : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Isluxury", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Isluxury");
        }
    }
}
