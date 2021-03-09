namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSliderTableExtentions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sliders", "SliderImageUrl", c => c.String());
            DropColumn("dbo.Sliders", "ImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sliders", "ImageUrl", c => c.String());
            DropColumn("dbo.Sliders", "SliderImageUrl");
        }
    }
}
