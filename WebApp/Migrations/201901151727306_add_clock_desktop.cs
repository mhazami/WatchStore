namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_clock_desktop : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clocks",
                c => new
                    {
                        ClockId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ClockId);
            
            CreateTable(
                "dbo.Desktops",
                c => new
                    {
                        DesktopId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.DesktopId);
            
            AddColumn("dbo.Products", "DesktopId", c => c.Guid());
            AddColumn("dbo.Products", "ClockId", c => c.Guid());
            AlterColumn("dbo.Products", "Off", c => c.Int());
            AlterColumn("dbo.Products", "Code", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "IsDeals", c => c.Boolean());
            AlterColumn("dbo.Products", "IsNewSeason", c => c.Boolean());
            AlterColumn("dbo.Products", "IsBlocked", c => c.Boolean());
            AlterColumn("dbo.Products", "Isluxury", c => c.Boolean());
            AlterColumn("dbo.Products", "WatchType", c => c.Byte());
            AlterColumn("dbo.Products", "MasterCategory", c => c.Byte());
            AlterColumn("dbo.Products", "Sex", c => c.Byte());
            AlterColumn("dbo.Products", "Orginal", c => c.Byte());
            AlterColumn("dbo.Products", "Motor", c => c.Byte());
            AlterColumn("dbo.Products", "ThicknessBody", c => c.Int());
            AlterColumn("dbo.Products", "FrameWidth", c => c.Int());
            AlterColumn("dbo.Products", "BodyWidth", c => c.Int());
            AlterColumn("dbo.Products", "Cranograph", c => c.Boolean());
            AlterColumn("dbo.Products", "WaterProf", c => c.Int());
            AlterColumn("dbo.Products", "Weight", c => c.Int());
            CreateIndex("dbo.Products", "DesktopId");
            CreateIndex("dbo.Products", "ClockId");
            AddForeignKey("dbo.Products", "ClockId", "dbo.Clocks", "ClockId");
            AddForeignKey("dbo.Products", "DesktopId", "dbo.Desktops", "DesktopId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "DesktopId", "dbo.Desktops");
            DropForeignKey("dbo.Products", "ClockId", "dbo.Clocks");
            DropIndex("dbo.Products", new[] { "ClockId" });
            DropIndex("dbo.Products", new[] { "DesktopId" });
            AlterColumn("dbo.Products", "Weight", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "WaterProf", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "Cranograph", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Products", "BodyWidth", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "FrameWidth", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "ThicknessBody", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "Motor", c => c.Byte(nullable: false));
            AlterColumn("dbo.Products", "Orginal", c => c.Byte(nullable: false));
            AlterColumn("dbo.Products", "Sex", c => c.Byte(nullable: false));
            AlterColumn("dbo.Products", "MasterCategory", c => c.Byte(nullable: false));
            AlterColumn("dbo.Products", "WatchType", c => c.Byte(nullable: false));
            AlterColumn("dbo.Products", "Isluxury", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Products", "IsBlocked", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Products", "IsNewSeason", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Products", "IsDeals", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Products", "Code", c => c.String());
            AlterColumn("dbo.Products", "Off", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "ClockId");
            DropColumn("dbo.Products", "DesktopId");
            DropTable("dbo.Desktops");
            DropTable("dbo.Clocks");
        }
    }
}
