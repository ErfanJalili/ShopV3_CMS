namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSkuTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SKUs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ManagerName = c.String(),
                        Description = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Mobile = c.String(),
                        WebSite = c.String(),
                        Instagram = c.String(),
                        Facebook = c.String(),
                        Telegram = c.String(),
                        Whatsapp = c.String(),
                        StartTime = c.String(),
                        EndTime = c.String(),
                        GoogleLocation = c.String(),
                        NeshanLocation = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SKUs");
        }
    }
}
