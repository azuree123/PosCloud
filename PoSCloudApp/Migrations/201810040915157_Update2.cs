namespace PoSCloudApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("PosCloud.Cities", "CreatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("PosCloud.Cities", "UpdatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("PosCloud.States", "CreatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("PosCloud.States", "UpdatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Customers", "CreatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Customers", "UpdatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Departments", "CreatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Departments", "UpdatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Designations", "CreatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Designations", "UpdatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Employees", "CreatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Employees", "UpdatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ExpenseHeads", "CreatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ExpenseHeads", "UpdatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Expenses", "CreatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Expenses", "UpdatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Locations", "CreatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Locations", "UpdatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ProductCategories", "CreatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ProductCategories", "UpdatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Products", "CreatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Products", "UpdatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.PurchaseOrderDetails", "CreatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.PurchaseOrderDetails", "UpdatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.SaleOrderDetails", "CreatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.SaleOrderDetails", "UpdatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.SaleOrders", "CreatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.SaleOrders", "UpdatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Suppliers", "CreatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Suppliers", "UpdatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Suppliers", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Suppliers", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SaleOrders", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SaleOrders", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SaleOrderDetails", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SaleOrderDetails", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PurchaseOrderDetails", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PurchaseOrderDetails", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Products", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Products", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ProductCategories", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ProductCategories", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Locations", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Locations", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Expenses", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Expenses", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ExpenseHeads", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ExpenseHeads", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Employees", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Employees", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Designations", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Designations", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Departments", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Departments", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Customers", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Customers", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("PosCloud.States", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("PosCloud.States", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("PosCloud.Cities", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("PosCloud.Cities", "CreatedOn", c => c.DateTime(nullable: false));
        }
    }
}
