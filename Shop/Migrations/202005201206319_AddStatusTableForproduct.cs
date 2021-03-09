namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusTableForproduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Products", "StatusItem");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "StatusItem", c => c.Int(nullable: false));
            DropTable("dbo.Status");
        }
    }
}
