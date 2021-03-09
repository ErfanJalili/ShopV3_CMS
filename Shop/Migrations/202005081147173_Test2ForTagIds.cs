namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test2ForTagIds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "SelectedTagIds", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "SelectedTagIds");
        }
    }
}
