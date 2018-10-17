namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial3 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("PosCloud.Customers", "StoreId");
            CreateIndex("PosCloud.SaleOrderDetails", "StoreId");
            CreateIndex("PosCloud.Products", "StoreId");
            CreateIndex("PosCloud.ProductCategories", "StoreId");
            CreateIndex("PosCloud.Employees", "StoreId");
            CreateIndex("PosCloud.ExpenseHeads", "StoreId");
            CreateIndex("PosCloud.PurchaseOrders", "StoreId");
            CreateIndex("PosCloud.Suppliers", "StoreId");
            AddForeignKey("PosCloud.Customers", "StoreId", "dbo.Stores", "Id");
            AddForeignKey("PosCloud.ExpenseHeads", "StoreId", "dbo.Stores", "Id");
            AddForeignKey("PosCloud.Expenses", "StoreId", "dbo.Stores", "Id");
            AddForeignKey("PosCloud.Employees", "StoreId", "dbo.Stores", "Id");
            AddForeignKey("PosCloud.Products", "StoreId", "dbo.Stores", "Id");
            AddForeignKey("PosCloud.PurchaseOrders", "StoreId", "dbo.Stores", "Id");
            AddForeignKey("PosCloud.Suppliers", "StoreId", "dbo.Stores", "Id");
            AddForeignKey("PosCloud.PurchaseOrderDetails", "StoreId", "dbo.Stores", "Id");
            AddForeignKey("PosCloud.SaleOrderDetails", "StoreId", "dbo.Stores", "Id");
            AddForeignKey("PosCloud.SaleOrders", "StoreId", "dbo.Stores", "Id");
            AddForeignKey("PosCloud.ProductCategories", "StoreId", "dbo.Stores", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("PosCloud.ProductCategories", "StoreId", "dbo.Stores");
            DropForeignKey("PosCloud.SaleOrders", "StoreId", "dbo.Stores");
            DropForeignKey("PosCloud.SaleOrderDetails", "StoreId", "dbo.Stores");
            DropForeignKey("PosCloud.PurchaseOrderDetails", "StoreId", "dbo.Stores");
            DropForeignKey("PosCloud.Suppliers", "StoreId", "dbo.Stores");
            DropForeignKey("PosCloud.PurchaseOrders", "StoreId", "dbo.Stores");
            DropForeignKey("PosCloud.Products", "StoreId", "dbo.Stores");
            DropForeignKey("PosCloud.Employees", "StoreId", "dbo.Stores");
            DropForeignKey("PosCloud.Expenses", "StoreId", "dbo.Stores");
            DropForeignKey("PosCloud.ExpenseHeads", "StoreId", "dbo.Stores");
            DropForeignKey("PosCloud.Customers", "StoreId", "dbo.Stores");
            DropIndex("PosCloud.Suppliers", new[] { "StoreId" });
            DropIndex("PosCloud.PurchaseOrders", new[] { "StoreId" });
            DropIndex("PosCloud.ExpenseHeads", new[] { "StoreId" });
            DropIndex("PosCloud.Employees", new[] { "StoreId" });
            DropIndex("PosCloud.ProductCategories", new[] { "StoreId" });
            DropIndex("PosCloud.Products", new[] { "StoreId" });
            DropIndex("PosCloud.SaleOrderDetails", new[] { "StoreId" });
            DropIndex("PosCloud.Customers", new[] { "StoreId" });
        }
    }
}
