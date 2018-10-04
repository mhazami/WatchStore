namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDTO : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CategoryId" });
            AddColumn("dbo.Baskets", "IsArchive", c => c.Boolean());
            AddColumn("dbo.Products", "Code", c => c.String());
            DropColumn("dbo.Baskets", "Quantity");
            DropColumn("dbo.Products", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "CategoryId", c => c.Guid(nullable: false));
            AddColumn("dbo.Baskets", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "Code");
            DropColumn("dbo.Baskets", "IsArchive");
            CreateIndex("dbo.Products", "CategoryId");
            AddForeignKey("dbo.Products", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
    }
}
