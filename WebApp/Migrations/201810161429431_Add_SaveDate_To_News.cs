namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_SaveDate_To_News : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "SaveDate", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "SaveDate");
        }
    }
}
