namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsActiveAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.BusinessPartners", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Stores", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Devices", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.DineTables", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Floors", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Employees", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Departments", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Expenses", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.ExpenseHeads", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Products", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.ProductsSubs", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.ProductCategories", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Units", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Sections", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Shifts", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Taxes", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.TransDetails", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.TransMaster", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.ReportLogs", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.SecurityObjects", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Cities", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.States", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Clients", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Locations", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.ProductCategoryGroups", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("PosCloud.ProductCategoryGroups", "IsActive");
            DropColumn("PosCloud.Locations", "IsActive");
            DropColumn("PosCloud.Clients", "IsActive");
            DropColumn("PosCloud.States", "IsActive");
            DropColumn("PosCloud.Cities", "IsActive");
            DropColumn("PosCloud.SecurityObjects", "IsActive");
            DropColumn("PosCloud.ReportLogs", "IsActive");
            DropColumn("PosCloud.TransMaster", "IsActive");
            DropColumn("PosCloud.TransDetails", "IsActive");
            DropColumn("PosCloud.Taxes", "IsActive");
            DropColumn("PosCloud.Shifts", "IsActive");
            DropColumn("PosCloud.Sections", "IsActive");
            DropColumn("PosCloud.Units", "IsActive");
            DropColumn("PosCloud.ProductCategories", "IsActive");
            DropColumn("PosCloud.ProductsSubs", "IsActive");
            DropColumn("PosCloud.Products", "IsActive");
            DropColumn("PosCloud.ExpenseHeads", "IsActive");
            DropColumn("PosCloud.Expenses", "IsActive");
            DropColumn("PosCloud.Departments", "IsActive");
            DropColumn("PosCloud.Employees", "IsActive");
            DropColumn("PosCloud.Floors", "IsActive");
            DropColumn("PosCloud.DineTables", "IsActive");
            DropColumn("PosCloud.Devices", "IsActive");
            DropColumn("PosCloud.Stores", "IsActive");
            DropColumn("PosCloud.BusinessPartners", "IsActive");
        }
    }
}
