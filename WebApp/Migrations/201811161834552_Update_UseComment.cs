namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_UseComment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserComments", "IsAccept", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserComments", "IsAccept");
        }
    }
}
