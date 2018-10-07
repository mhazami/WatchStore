namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_cantact : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Cantacts", "Subject");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cantacts", "Subject", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
