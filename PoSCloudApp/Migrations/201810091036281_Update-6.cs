namespace PoSCloudApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update6 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.PurchaseOrderDetails", "PurchaseOrderId");
            CreateIndex("dbo.PurchaseOrderDetails", "ProductId");
            CreateIndex("dbo.PurchaseOrders", "SupplierId");
            AddForeignKey("dbo.PurchaseOrderDetails", "ProductId", "dbo.Products", "Id");
            AddForeignKey("dbo.PurchaseOrderDetails", "PurchaseOrderId", "dbo.PurchaseOrders", "Id");
            AddForeignKey("dbo.PurchaseOrders", "SupplierId", "dbo.Suppliers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseOrders", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.PurchaseOrderDetails", "PurchaseOrderId", "dbo.PurchaseOrders");
            DropForeignKey("dbo.PurchaseOrderDetails", "ProductId", "dbo.Products");
            DropIndex("dbo.PurchaseOrders", new[] { "SupplierId" });
            DropIndex("dbo.PurchaseOrderDetails", new[] { "ProductId" });
            DropIndex("dbo.PurchaseOrderDetails", new[] { "PurchaseOrderId" });
        }
    }
}
