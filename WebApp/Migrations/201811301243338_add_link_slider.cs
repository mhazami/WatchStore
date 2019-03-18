namespace ClockStore.DTO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_link_slider : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sliders", "Link", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sliders", "Link");
        }
    }
}
