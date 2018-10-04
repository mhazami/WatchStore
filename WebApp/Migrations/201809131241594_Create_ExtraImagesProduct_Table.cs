namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_ExtraImagesProduct_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExtraImagesProducts",
                c => new
                    {
                        ExtraImagesProductId = c.Guid(nullable: false),
                        FileId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ExtraImagesProductId)
                .ForeignKey("dbo.Files", t => t.FileId, cascadeDelete: true)
                .Index(t => t.FileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExtraImagesProducts", "FileId", "dbo.Files");
            DropIndex("dbo.ExtraImagesProducts", new[] { "FileId" });
            DropTable("dbo.ExtraImagesProducts");
        }
    }
}
