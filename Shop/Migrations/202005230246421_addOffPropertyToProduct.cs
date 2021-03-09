namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addOffPropertyToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "OffStart", c => c.DateTime());
            AddColumn("dbo.Products", "OffEnd", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "OffEnd");
            DropColumn("dbo.Products", "OffStart");
        }
    }
}
