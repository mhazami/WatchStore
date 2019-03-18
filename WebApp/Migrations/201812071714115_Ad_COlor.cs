namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ad_COlor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Value = c.String(maxLength: 100),
                        ProductId = c.Guid(nullable: false),
                        Enable = c.Boolean(),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Colors");
        }
    }
}
