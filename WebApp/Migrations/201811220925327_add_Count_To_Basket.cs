namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_Count_To_Basket : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Baskets", "Count", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Baskets", "Count");
        }
    }
}
