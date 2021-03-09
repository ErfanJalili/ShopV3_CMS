namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropToCustomerTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "sad", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "sad");
        }
    }
}
