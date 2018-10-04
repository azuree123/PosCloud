namespace PoSCloudApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PurchaseOderDetails", newName: "PurchaseOrderDetails");
            AddColumn("dbo.Products", "ProductCategory_Id", c => c.Int());
            CreateIndex("dbo.Products", "ProductCategory_Id");
            AddForeignKey("dbo.Products", "ProductCategory_Id", "dbo.ProductCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ProductCategory_Id", "dbo.ProductCategories");
            DropIndex("dbo.Products", new[] { "ProductCategory_Id" });
            DropColumn("dbo.Products", "ProductCategory_Id");
            RenameTable(name: "dbo.PurchaseOrderDetails", newName: "PurchaseOderDetails");
        }
    }
}
