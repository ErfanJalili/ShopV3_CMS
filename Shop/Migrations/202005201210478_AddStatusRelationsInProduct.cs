namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusRelationsInProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "StatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "StatusId");
            AddForeignKey("dbo.Products", "StatusId", "dbo.Status", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "StatusId", "dbo.Status");
            DropIndex("dbo.Products", new[] { "StatusId" });
            DropColumn("dbo.Products", "StatusId");
        }
    }
}
