namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_File_To_Catgory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "FileId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "FileId");
        }
    }
}
