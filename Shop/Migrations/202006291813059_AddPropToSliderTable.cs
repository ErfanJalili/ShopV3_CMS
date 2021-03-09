namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropToSliderTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sliders", "Description", c => c.String());
            AddColumn("dbo.Sliders", "Alt", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sliders", "Alt");
            DropColumn("dbo.Sliders", "Description");
        }
    }
}
