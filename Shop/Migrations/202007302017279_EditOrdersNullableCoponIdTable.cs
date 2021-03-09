namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditOrdersNullableCoponIdTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "OrderCoponId", "dbo.OrderCopons");
            DropIndex("dbo.Orders", new[] { "OrderCoponId" });
            AlterColumn("dbo.Orders", "OrderCoponId", c => c.Int());
            CreateIndex("dbo.Orders", "OrderCoponId");
            AddForeignKey("dbo.Orders", "OrderCoponId", "dbo.OrderCopons", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "OrderCoponId", "dbo.OrderCopons");
            DropIndex("dbo.Orders", new[] { "OrderCoponId" });
            AlterColumn("dbo.Orders", "OrderCoponId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "OrderCoponId");
            AddForeignKey("dbo.Orders", "OrderCoponId", "dbo.OrderCopons", "Id", cascadeDelete: true);
        }
    }
}
