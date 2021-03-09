namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNumberAvailableToProductTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "NumberAvailable", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "NumberAvailable");
        }
    }
}
