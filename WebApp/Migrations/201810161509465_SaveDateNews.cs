namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SaveDateNews : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.News", "SaveDate", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.News", "SaveDate", c => c.DateTime(nullable: false));
        }
    }
}
