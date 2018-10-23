namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.Cities", "Synced", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Cities", "SyncedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("PosCloud.Cities", "Code", c => c.String(maxLength: 150, unicode: false));
            AddColumn("PosCloud.States", "Synced", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.States", "SyncedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("PosCloud.States", "Code", c => c.String(maxLength: 150, unicode: false));
            AddColumn("PosCloud.Coupons", "Synced", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Coupons", "SyncedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("PosCloud.Stores", "Synced", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Stores", "SyncedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("PosCloud.Stores", "Code", c => c.String(maxLength: 150, unicode: false));
            AddColumn("PosCloud.Customers", "Synced", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Customers", "SyncedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("PosCloud.Customers", "Code", c => c.String(maxLength: 150, unicode: false));
            AddColumn("PosCloud.SaleOrders", "Synced", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.SaleOrders", "SyncedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("PosCloud.SaleOrderDetails", "Synced", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.SaleOrderDetails", "SyncedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("PosCloud.SaleOrderDetails", "Code", c => c.String(maxLength: 150, unicode: false));
            AddColumn("PosCloud.Products", "Synced", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Products", "SyncedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("PosCloud.Products", "Code", c => c.String(maxLength: 150, unicode: false));
            AddColumn("PosCloud.ProductCategories", "Synced", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.ProductCategories", "SyncedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("PosCloud.ProductCategories", "Code", c => c.String(maxLength: 150, unicode: false));
            AddColumn("PosCloud.PurchaseOrderDetails", "Synced", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.PurchaseOrderDetails", "SyncedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("PosCloud.PurchaseOrderDetails", "Code", c => c.String(maxLength: 150, unicode: false));
            AddColumn("PosCloud.PurchaseOrders", "Synced", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.PurchaseOrders", "SyncedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("PosCloud.PurchaseOrders", "Code", c => c.String(maxLength: 150, unicode: false));
            AddColumn("PosCloud.Suppliers", "Synced", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Suppliers", "SyncedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("PosCloud.Suppliers", "Code", c => c.String(maxLength: 150, unicode: false));
            AddColumn("PosCloud.Discounts", "Synced", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Discounts", "SyncedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("PosCloud.Discounts", "Code", c => c.String(maxLength: 150, unicode: false));
            AddColumn("PosCloud.Employees", "Synced", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Employees", "SyncedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("PosCloud.Employees", "Code", c => c.String(maxLength: 150, unicode: false));
            AddColumn("PosCloud.Departments", "Synced", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Departments", "SyncedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("PosCloud.Departments", "Code", c => c.String(maxLength: 150, unicode: false));
            AddColumn("PosCloud.Designations", "Synced", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Designations", "SyncedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("PosCloud.Designations", "Code", c => c.String(maxLength: 150, unicode: false));
            AddColumn("PosCloud.Expenses", "Synced", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Expenses", "SyncedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("PosCloud.Expenses", "Code", c => c.String(maxLength: 150, unicode: false));
            AddColumn("PosCloud.ExpenseHeads", "Synced", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.ExpenseHeads", "SyncedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("PosCloud.ExpenseHeads", "Code", c => c.String(maxLength: 150, unicode: false));
            AddColumn("PosCloud.Taxes", "Synced", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Taxes", "SyncedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("PosCloud.Taxes", "Code", c => c.String(maxLength: 150, unicode: false));
            AddColumn("PosCloud.Locations", "Synced", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Locations", "SyncedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("PosCloud.Locations", "Code", c => c.String(maxLength: 150, unicode: false));
            AlterColumn("PosCloud.Coupons", "Code", c => c.String(maxLength: 150, unicode: false));
            DropColumn("PosCloud.Products", "ProductCode");
        }
        
        public override void Down()
        {
            AddColumn("PosCloud.Products", "ProductCode", c => c.String(maxLength: 150));
            AlterColumn("PosCloud.Coupons", "Code", c => c.String(maxLength: 4, unicode: false));
            DropColumn("PosCloud.Locations", "Code");
            DropColumn("PosCloud.Locations", "SyncedOn");
            DropColumn("PosCloud.Locations", "Synced");
            DropColumn("PosCloud.Taxes", "Code");
            DropColumn("PosCloud.Taxes", "SyncedOn");
            DropColumn("PosCloud.Taxes", "Synced");
            DropColumn("PosCloud.ExpenseHeads", "Code");
            DropColumn("PosCloud.ExpenseHeads", "SyncedOn");
            DropColumn("PosCloud.ExpenseHeads", "Synced");
            DropColumn("PosCloud.Expenses", "Code");
            DropColumn("PosCloud.Expenses", "SyncedOn");
            DropColumn("PosCloud.Expenses", "Synced");
            DropColumn("PosCloud.Designations", "Code");
            DropColumn("PosCloud.Designations", "SyncedOn");
            DropColumn("PosCloud.Designations", "Synced");
            DropColumn("PosCloud.Departments", "Code");
            DropColumn("PosCloud.Departments", "SyncedOn");
            DropColumn("PosCloud.Departments", "Synced");
            DropColumn("PosCloud.Employees", "Code");
            DropColumn("PosCloud.Employees", "SyncedOn");
            DropColumn("PosCloud.Employees", "Synced");
            DropColumn("PosCloud.Discounts", "Code");
            DropColumn("PosCloud.Discounts", "SyncedOn");
            DropColumn("PosCloud.Discounts", "Synced");
            DropColumn("PosCloud.Suppliers", "Code");
            DropColumn("PosCloud.Suppliers", "SyncedOn");
            DropColumn("PosCloud.Suppliers", "Synced");
            DropColumn("PosCloud.PurchaseOrders", "Code");
            DropColumn("PosCloud.PurchaseOrders", "SyncedOn");
            DropColumn("PosCloud.PurchaseOrders", "Synced");
            DropColumn("PosCloud.PurchaseOrderDetails", "Code");
            DropColumn("PosCloud.PurchaseOrderDetails", "SyncedOn");
            DropColumn("PosCloud.PurchaseOrderDetails", "Synced");
            DropColumn("PosCloud.ProductCategories", "Code");
            DropColumn("PosCloud.ProductCategories", "SyncedOn");
            DropColumn("PosCloud.ProductCategories", "Synced");
            DropColumn("PosCloud.Products", "Code");
            DropColumn("PosCloud.Products", "SyncedOn");
            DropColumn("PosCloud.Products", "Synced");
            DropColumn("PosCloud.SaleOrderDetails", "Code");
            DropColumn("PosCloud.SaleOrderDetails", "SyncedOn");
            DropColumn("PosCloud.SaleOrderDetails", "Synced");
            DropColumn("PosCloud.SaleOrders", "SyncedOn");
            DropColumn("PosCloud.SaleOrders", "Synced");
            DropColumn("PosCloud.Customers", "Code");
            DropColumn("PosCloud.Customers", "SyncedOn");
            DropColumn("PosCloud.Customers", "Synced");
            DropColumn("PosCloud.Stores", "Code");
            DropColumn("PosCloud.Stores", "SyncedOn");
            DropColumn("PosCloud.Stores", "Synced");
            DropColumn("PosCloud.Coupons", "SyncedOn");
            DropColumn("PosCloud.Coupons", "Synced");
            DropColumn("PosCloud.States", "Code");
            DropColumn("PosCloud.States", "SyncedOn");
            DropColumn("PosCloud.States", "Synced");
            DropColumn("PosCloud.Cities", "Code");
            DropColumn("PosCloud.Cities", "SyncedOn");
            DropColumn("PosCloud.Cities", "Synced");
        }
    }
}
