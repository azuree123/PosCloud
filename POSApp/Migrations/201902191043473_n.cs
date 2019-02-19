namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class n : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.BusinessPartners", "ArabicName", c => c.String(maxLength: 150));
            AddColumn("PosCloud.Stores", "ArabicName", c => c.String(maxLength: 150));
            AddColumn("PosCloud.Clients", "ArabicName", c => c.String(maxLength: 150));
            AddColumn("PosCloud.Devices", "ArabicName", c => c.String(maxLength: 150));
            AddColumn("PosCloud.TimedEvents", "ArabicName", c => c.String(maxLength: 150));
            AddColumn("PosCloud.Products", "ArabicName", c => c.String(maxLength: 150));
            AddColumn("PosCloud.Modifier", "ArabicName", c => c.String(maxLength: 150));
            AddColumn("PosCloud.ModifierOptions", "ArabicName", c => c.String(maxLength: 150));
            AddColumn("PosCloud.Taxes", "ArabicName", c => c.String(maxLength: 150));
            AddColumn("PosCloud.ProductCategories", "ArabicName", c => c.String(maxLength: 150));
            AddColumn("PosCloud.Units", "ArabicName", c => c.String(maxLength: 150));
            AddColumn("PosCloud.Sections", "ArabicName", c => c.String(maxLength: 150));
            AddColumn("PosCloud.POSTerminals", "ArabicName", c => c.String(maxLength: 150));
            AddColumn("PosCloud.Employees", "ArabicName", c => c.String(maxLength: 150));
            AddColumn("PosCloud.Departments", "ArabicName", c => c.String(maxLength: 150));
            AddColumn("PosCloud.Designations", "ArabicName", c => c.String(maxLength: 150));
            AddColumn("PosCloud.ExpenseHeads", "ArabicName", c => c.String(maxLength: 150));
            AddColumn("PosCloud.Shifts", "ArabicName", c => c.String(maxLength: 150));
            AddColumn("PosCloud.Warehouses", "ArabicName", c => c.String(maxLength: 150));
            AddColumn("PosCloud.Cities", "ArabicName", c => c.String(maxLength: 150));
            AddColumn("PosCloud.States", "ArabicName", c => c.String(maxLength: 150));
            AddColumn("PosCloud.Locations", "ArabicName", c => c.String());
            AddColumn("PosCloud.ProductCategoryGroups", "ArabicName", c => c.String());
            AddColumn("PosCloud.Sizes", "ArabicName", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("PosCloud.Sizes", "ArabicName");
            DropColumn("PosCloud.ProductCategoryGroups", "ArabicName");
            DropColumn("PosCloud.Locations", "ArabicName");
            DropColumn("PosCloud.States", "ArabicName");
            DropColumn("PosCloud.Cities", "ArabicName");
            DropColumn("PosCloud.Warehouses", "ArabicName");
            DropColumn("PosCloud.Shifts", "ArabicName");
            DropColumn("PosCloud.ExpenseHeads", "ArabicName");
            DropColumn("PosCloud.Designations", "ArabicName");
            DropColumn("PosCloud.Departments", "ArabicName");
            DropColumn("PosCloud.Employees", "ArabicName");
            DropColumn("PosCloud.POSTerminals", "ArabicName");
            DropColumn("PosCloud.Sections", "ArabicName");
            DropColumn("PosCloud.Units", "ArabicName");
            DropColumn("PosCloud.ProductCategories", "ArabicName");
            DropColumn("PosCloud.Taxes", "ArabicName");
            DropColumn("PosCloud.ModifierOptions", "ArabicName");
            DropColumn("PosCloud.Modifier", "ArabicName");
            DropColumn("PosCloud.Products", "ArabicName");
            DropColumn("PosCloud.TimedEvents", "ArabicName");
            DropColumn("PosCloud.Devices", "ArabicName");
            DropColumn("PosCloud.Clients", "ArabicName");
            DropColumn("PosCloud.Stores", "ArabicName");
            DropColumn("PosCloud.BusinessPartners", "ArabicName");
        }
    }
}
