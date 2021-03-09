namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveTagIdToListOfIds : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.Products", new[] { "Tag_Id" });
            RenameColumn(table: "dbo.Products", name: "Tag_Id", newName: "TagId");
            AlterColumn("dbo.Products", "TagId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "TagId");
            AddForeignKey("dbo.Products", "TagId", "dbo.Tags", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "TagId", "dbo.Tags");
            DropIndex("dbo.Products", new[] { "TagId" });
            AlterColumn("dbo.Products", "TagId", c => c.Int());
            RenameColumn(table: "dbo.Products", name: "TagId", newName: "Tag_Id");
            CreateIndex("dbo.Products", "Tag_Id");
            AddForeignKey("dbo.Products", "Tag_Id", "dbo.Tags", "Id");
        }
    }
}
