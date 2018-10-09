namespace PoSCloudApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SaleOrders", "Employee_Id", c => c.Int());
            CreateIndex("dbo.SaleOrders", "CustomerId");
            CreateIndex("dbo.SaleOrders", "Employee_Id");
            CreateIndex("dbo.SaleOrderDetails", "SaleOrderId");
            CreateIndex("dbo.SaleOrderDetails", "ProductId");
            AddForeignKey("dbo.SaleOrders", "CustomerId", "dbo.Customers", "Id");
            AddForeignKey("dbo.SaleOrders", "Employee_Id", "dbo.Employees", "Id");
            AddForeignKey("dbo.SaleOrderDetails", "ProductId", "dbo.Products", "Id");
            AddForeignKey("dbo.SaleOrderDetails", "SaleOrderId", "dbo.SaleOrders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SaleOrderDetails", "SaleOrderId", "dbo.SaleOrders");
            DropForeignKey("dbo.SaleOrderDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.SaleOrders", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.SaleOrders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.SaleOrderDetails", new[] { "ProductId" });
            DropIndex("dbo.SaleOrderDetails", new[] { "SaleOrderId" });
            DropIndex("dbo.SaleOrders", new[] { "Employee_Id" });
            DropIndex("dbo.SaleOrders", new[] { "CustomerId" });
            DropColumn("dbo.SaleOrders", "Employee_Id");
        }
    }
}
