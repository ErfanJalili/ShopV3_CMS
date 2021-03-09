namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditOrdersUserId3TypeTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "UserId");
        }
    }
}
