namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeStatusFromStringToEnum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "StatusItem", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Status", c => c.String());
            DropColumn("dbo.Products", "StatusItem");
        }
    }
}
