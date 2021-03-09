namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSkuRelationToProductTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "SKUId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "SKUId");
            AddForeignKey("dbo.Products", "SKUId", "dbo.SKUs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "SKUId", "dbo.SKUs");
            DropIndex("dbo.Products", new[] { "SKUId" });
            DropColumn("dbo.Products", "SKUId");
        }
    }
}
