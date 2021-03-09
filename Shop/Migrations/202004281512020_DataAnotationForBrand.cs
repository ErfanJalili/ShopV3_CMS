namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataAnotationForBrand : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Brands", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Brands", "Name", c => c.String());
        }
    }
}
