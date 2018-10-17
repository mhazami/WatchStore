namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SaveDatNews_To_DateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.News", "SaveDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.News", "SaveDate", c => c.String(maxLength: 10));
        }
    }
}
