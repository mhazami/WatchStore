namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_LangId_To_News : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "LangId", c => c.String(nullable: false, maxLength: 5));
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "LangId");
        }
    }
}
