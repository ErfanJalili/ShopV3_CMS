namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTagIdToListOfIds : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "TagId", "dbo.Tags");
            DropIndex("dbo.Products", new[] { "TagId" });
            RenameColumn(table: "dbo.Products", name: "TagId", newName: "Tag_Id");
            AlterColumn("dbo.Products", "Tag_Id", c => c.Int());
            CreateIndex("dbo.Products", "Tag_Id");
            AddForeignKey("dbo.Products", "Tag_Id", "dbo.Tags", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.Products", new[] { "Tag_Id" });
            AlterColumn("dbo.Products", "Tag_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Products", name: "Tag_Id", newName: "TagId");
            CreateIndex("dbo.Products", "TagId");
            AddForeignKey("dbo.Products", "TagId", "dbo.Tags", "Id", cascadeDelete: true);
        }
    }
}
