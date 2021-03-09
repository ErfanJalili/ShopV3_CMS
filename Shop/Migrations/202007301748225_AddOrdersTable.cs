namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrdersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FactorNumber = c.String(),
                        UserId = c.Int(nullable: false),
                        UserCompany = c.String(),
                        Country = c.String(),
                        city = c.String(),
                        State = c.String(),
                        UserStreet = c.String(),
                        UserSuburb = c.String(),
                        Code = c.String(),
                        PostCode = c.String(),
                        Telephone = c.String(),
                        Mobile = c.String(),
                        Email = c.String(),
                        Created_at = c.DateTime(),
                        OrderStatusId = c.Int(nullable: false),
                        OrderCoponId = c.Int(nullable: false),
                        TotalPrice = c.String(),
                        PaymentRecipe = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderCopons", t => t.OrderCoponId, cascadeDelete: true)
                .ForeignKey("dbo.OrderStatus", t => t.OrderStatusId, cascadeDelete: true)
                .Index(t => t.OrderStatusId)
                .Index(t => t.OrderCoponId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "OrderStatusId", "dbo.OrderStatus");
            DropForeignKey("dbo.Orders", "OrderCoponId", "dbo.OrderCopons");
            DropIndex("dbo.Orders", new[] { "OrderCoponId" });
            DropIndex("dbo.Orders", new[] { "OrderStatusId" });
            DropTable("dbo.Orders");
        }
    }
}
