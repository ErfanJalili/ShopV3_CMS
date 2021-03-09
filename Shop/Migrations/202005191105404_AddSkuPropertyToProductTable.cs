namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSkuPropertyToProductTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "SKU");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "SKU", c => c.Int(nullable: false));
        }
    }
}
