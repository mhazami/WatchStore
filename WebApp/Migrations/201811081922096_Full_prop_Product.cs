namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Full_prop_Product : DbMigration
    {
        public override void Up()
        {
            
            AddColumn("dbo.Products", "CategoryId", c => c.Guid(nullable: false,defaultValue: new Guid("ce826508-10de-43b5-9096-c4fc69f4c69f")));
            AddColumn("dbo.Products", "WatchType", c => c.Byte(nullable: false));
            AddColumn("dbo.Products", "Sex", c => c.Byte(nullable: false));
            AddColumn("dbo.Products", "Orginal", c => c.Byte(nullable: false));
            AddColumn("dbo.Products", "ManufacturingCountry", c => c.String());
            AddColumn("dbo.Products", "Motor", c => c.Byte(nullable: false));
            AddColumn("dbo.Products", "EnergySource", c => c.String());
            AddColumn("dbo.Products", "PageForm", c => c.String());
            AddColumn("dbo.Products", "Color", c => c.String());
            AddColumn("dbo.Products", "ThicknessBody", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "BackFrame", c => c.String());
            AddColumn("dbo.Products", "FrameWidth", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Glass", c => c.String());
            AddColumn("dbo.Products", "Jerk", c => c.String());
            AddColumn("dbo.Products", "JerkColor", c => c.String());
            AddColumn("dbo.Products", "BodyMaterial", c => c.String());
            AddColumn("dbo.Products", "BodyColor", c => c.String());
            AddColumn("dbo.Products", "BodyWidth", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "PageColor", c => c.String());
            AddColumn("dbo.Products", "LockType", c => c.Byte(nullable: false));
            AddColumn("dbo.Products", "Cranograph", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "WaterProf", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Weight", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "CategoryId");
            AddForeignKey("dbo.Products", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropColumn("dbo.Products", "Weight");
            DropColumn("dbo.Products", "WaterProf");
            DropColumn("dbo.Products", "Cranograph");
            DropColumn("dbo.Products", "LockType");
            DropColumn("dbo.Products", "PageColor");
            DropColumn("dbo.Products", "BodyWidth");
            DropColumn("dbo.Products", "BodyColor");
            DropColumn("dbo.Products", "BodyMaterial");
            DropColumn("dbo.Products", "JerkColor");
            DropColumn("dbo.Products", "Jerk");
            DropColumn("dbo.Products", "Glass");
            DropColumn("dbo.Products", "FrameWidth");
            DropColumn("dbo.Products", "BackFrame");
            DropColumn("dbo.Products", "ThicknessBody");
            DropColumn("dbo.Products", "Color");
            DropColumn("dbo.Products", "PageForm");
            DropColumn("dbo.Products", "EnergySource");
            DropColumn("dbo.Products", "Motor");
            DropColumn("dbo.Products", "ManufacturingCountry");
            DropColumn("dbo.Products", "Orginal");
            DropColumn("dbo.Products", "Sex");
            DropColumn("dbo.Products", "WatchType");
            DropColumn("dbo.Products", "CategoryId");
        }
    }
}
