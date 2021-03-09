namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPercentPropToOrderCoponsModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderCopons", "Percent", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderCopons", "Percent");
        }
    }
}
