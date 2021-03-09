namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTellAndPhoneNumberAndAddressToBrandTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Brands", "Tell", c => c.String(maxLength: 11));
            AddColumn("dbo.Brands", "PhoneNumber", c => c.String(maxLength: 11));
            AddColumn("dbo.Brands", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Brands", "Address");
            DropColumn("dbo.Brands", "PhoneNumber");
            DropColumn("dbo.Brands", "Tell");
        }
    }
}
