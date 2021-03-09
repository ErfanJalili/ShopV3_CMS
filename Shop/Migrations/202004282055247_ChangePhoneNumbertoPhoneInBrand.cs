namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePhoneNumbertoPhoneInBrand : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Brands", "Phone", c => c.String(maxLength: 11));
            DropColumn("dbo.Brands", "PhoneNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Brands", "PhoneNumber", c => c.String(maxLength: 11));
            DropColumn("dbo.Brands", "Phone");
        }
    }
}
