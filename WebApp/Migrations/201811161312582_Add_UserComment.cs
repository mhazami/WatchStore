namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_UserComment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserComments",
                c => new
                    {
                        UserCommentsId = c.Long(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 200),
                        Title = c.String(nullable: false, maxLength: 400),
                        Message = c.String(nullable: false, maxLength: 4000),
                        ProductId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.UserCommentsId);
            
            AddColumn("dbo.Products", "Type", c => c.Byte(nullable: false));
            AddColumn("dbo.Products", "MasterCategory", c => c.Byte(nullable: false));
            AlterColumn("dbo.Products", "LockType", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "LockType", c => c.Byte(nullable: false));
            DropColumn("dbo.Products", "MasterCategory");
            DropColumn("dbo.Products", "Type");
            DropTable("dbo.UserComments");
        }
    }
}
