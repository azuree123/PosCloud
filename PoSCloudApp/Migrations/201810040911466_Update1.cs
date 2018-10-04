namespace PoSCloudApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("PosCloud.Cities", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("PosCloud.States", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Customers", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Departments", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Designations", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Employees", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ExpenseHeads", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Expenses", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Locations", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ProductCategories", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Products", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PurchaseOrderDetails", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SaleOrderDetails", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SaleOrders", "UpdatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Suppliers", "UpdatedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Suppliers", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.SaleOrders", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.SaleOrderDetails", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.PurchaseOrderDetails", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.Products", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.ProductCategories", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.Locations", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.Expenses", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.ExpenseHeads", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.Employees", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.Designations", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.Departments", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.Customers", "UpdatedOn", c => c.DateTime());
            AlterColumn("PosCloud.States", "UpdatedOn", c => c.DateTime());
            AlterColumn("PosCloud.Cities", "UpdatedOn", c => c.DateTime());
        }
    }
}
