namespace PoSCloudApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "PosCloud.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        StateId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false, maxLength: 150),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 150),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("PosCloud.States", t => t.StateId, cascadeDelete: true)
                .Index(t => t.StateId);
            
            CreateTable(
                "PosCloud.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false, maxLength: 150),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 150),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "PosCloud.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Email = c.String(maxLength: 150),
                        PhoneNumber = c.String(maxLength: 150),
                        Referral = c.String(maxLength: 150),
                        Gender = c.String(maxLength: 150),
                        Address = c.String(maxLength: 150),
                        State = c.String(maxLength: 150),
                        City = c.String(maxLength: 150),
                        Birthday = c.DateTime(),
                        Note = c.String(maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false, maxLength: 150),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 150),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "PosCloud.SaleOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 150),
                        Date = c.String(maxLength: 150),
                        Time = c.String(maxLength: 4000),
                        Amount = c.Double(),
                        Tax = c.Double(),
                        Discount = c.Double(),
                        Status = c.String(maxLength: 50),
                        Canceled = c.Boolean(),
                        Type = c.String(maxLength: 150),
                        CashierId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false, maxLength: 150),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 150),
                        Synced = c.Boolean(),
                        Employee_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("PosCloud.Customers", t => t.CustomerId)
                .ForeignKey("PosCloud.Employees", t => t.Employee_Id)
                .Index(t => t.CustomerId)
                .Index(t => t.Employee_Id);
            
            CreateTable(
                "PosCloud.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Email = c.String(maxLength: 150),
                        Gender = c.String(maxLength: 150),
                        MobileNumber = c.String(maxLength: 150),
                        Salary = c.Double(),
                        Commission = c.Double(),
                        JoinDate = c.DateTime(),
                        Booking = c.Boolean(),
                        DepartmentId = c.Int(nullable: false),
                        DesignationId = c.Int(nullable: false),
                        Address = c.String(maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false, maxLength: 150),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 150),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("PosCloud.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("PosCloud.Designations", t => t.DesignationId, cascadeDelete: true)
                .Index(t => t.DepartmentId)
                .Index(t => t.DesignationId);
            
            CreateTable(
                "PosCloud.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false, maxLength: 150),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 150),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "PosCloud.Designations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false, maxLength: 150),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 150),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "PosCloud.Expenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExpenseHeadId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        Amount = c.Double(),
                        Description = c.String(maxLength: 150),
                        Date = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false, maxLength: 150),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 150),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("PosCloud.Employees", t => t.EmployeeId)
                .ForeignKey("PosCloud.ExpenseHeads", t => t.ExpenseHeadId, cascadeDelete: true)
                .Index(t => t.ExpenseHeadId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "PosCloud.ExpenseHeads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Details = c.String(maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false, maxLength: 150),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 150),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "PosCloud.SaleOrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SaleOrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(),
                        Price = c.Double(),
                        Discount = c.Double(),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false, maxLength: 150),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 150),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("PosCloud.Products", t => t.ProductId)
                .ForeignKey("PosCloud.SaleOrders", t => t.SaleOrderId, cascadeDelete: true)
                .Index(t => t.SaleOrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "PosCloud.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        ProductCode = c.String(maxLength: 150),
                        CategoryId = c.Int(nullable: false),
                        Duration = c.String(maxLength: 4000),
                        Available = c.String(maxLength: 150),
                        SupplierId = c.Int(nullable: false),
                        Tax = c.Double(),
                        UnitPrice = c.Double(),
                        Stock = c.Double(),
                        Barcode = c.String(maxLength: 150),
                        Image = c.String(maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false, maxLength: 150),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 150),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("PosCloud.ProductCategories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("PosCloud.Suppliers", t => t.SupplierId)
                .Index(t => t.CategoryId)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "PosCloud.ProductCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Image = c.String(maxLength: 150),
                        Type = c.String(maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false, maxLength: 150),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 150),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "PosCloud.PurchaseOrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PurchaseOrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(),
                        RetailPrice = c.Double(),
                        Discount = c.Double(),
                        UnitPrice = c.Double(),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false, maxLength: 150),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 150),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("PosCloud.Products", t => t.ProductId)
                .ForeignKey("PosCloud.PurchaseOrders", t => t.PurchaseOrderId, cascadeDelete: true)
                .Index(t => t.PurchaseOrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "PosCloud.PurchaseOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SupplierId = c.Int(nullable: false),
                        OrderDate = c.DateTime(),
                        SupplyDate = c.DateTime(),
                        InvoiceId = c.Int(nullable: false),
                        TotalPrice = c.Double(),
                        Type = c.String(maxLength: 4000),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false, maxLength: 150),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 150),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("PosCloud.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "PosCloud.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Email = c.String(maxLength: 150),
                        PhoneNumber = c.String(maxLength: 150),
                        Address = c.String(maxLength: 4000),
                        Company = c.String(maxLength: 150),
                        State = c.String(maxLength: 50),
                        City = c.String(maxLength: 150),
                        Balance = c.Double(),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false, maxLength: 150),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 150),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "PosCloud.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Address = c.String(maxLength: 150),
                        Contact = c.String(maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(nullable: false, maxLength: 150),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 150),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("PosCloud.SaleOrderDetails", "SaleOrderId", "PosCloud.SaleOrders");
            DropForeignKey("PosCloud.SaleOrderDetails", "ProductId", "PosCloud.Products");
            DropForeignKey("PosCloud.Products", "SupplierId", "PosCloud.Suppliers");
            DropForeignKey("PosCloud.PurchaseOrderDetails", "PurchaseOrderId", "PosCloud.PurchaseOrders");
            DropForeignKey("PosCloud.PurchaseOrders", "SupplierId", "PosCloud.Suppliers");
            DropForeignKey("PosCloud.PurchaseOrderDetails", "ProductId", "PosCloud.Products");
            DropForeignKey("PosCloud.Products", "CategoryId", "PosCloud.ProductCategories");
            DropForeignKey("PosCloud.SaleOrders", "Employee_Id", "PosCloud.Employees");
            DropForeignKey("PosCloud.Expenses", "ExpenseHeadId", "PosCloud.ExpenseHeads");
            DropForeignKey("PosCloud.Expenses", "EmployeeId", "PosCloud.Employees");
            DropForeignKey("PosCloud.Employees", "DesignationId", "PosCloud.Designations");
            DropForeignKey("PosCloud.Employees", "DepartmentId", "PosCloud.Departments");
            DropForeignKey("PosCloud.SaleOrders", "CustomerId", "PosCloud.Customers");
            DropForeignKey("PosCloud.Cities", "StateId", "PosCloud.States");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("PosCloud.PurchaseOrders", new[] { "SupplierId" });
            DropIndex("PosCloud.PurchaseOrderDetails", new[] { "ProductId" });
            DropIndex("PosCloud.PurchaseOrderDetails", new[] { "PurchaseOrderId" });
            DropIndex("PosCloud.Products", new[] { "SupplierId" });
            DropIndex("PosCloud.Products", new[] { "CategoryId" });
            DropIndex("PosCloud.SaleOrderDetails", new[] { "ProductId" });
            DropIndex("PosCloud.SaleOrderDetails", new[] { "SaleOrderId" });
            DropIndex("PosCloud.Expenses", new[] { "EmployeeId" });
            DropIndex("PosCloud.Expenses", new[] { "ExpenseHeadId" });
            DropIndex("PosCloud.Employees", new[] { "DesignationId" });
            DropIndex("PosCloud.Employees", new[] { "DepartmentId" });
            DropIndex("PosCloud.SaleOrders", new[] { "Employee_Id" });
            DropIndex("PosCloud.SaleOrders", new[] { "CustomerId" });
            DropIndex("PosCloud.Cities", new[] { "StateId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("PosCloud.Locations");
            DropTable("PosCloud.Suppliers");
            DropTable("PosCloud.PurchaseOrders");
            DropTable("PosCloud.PurchaseOrderDetails");
            DropTable("PosCloud.ProductCategories");
            DropTable("PosCloud.Products");
            DropTable("PosCloud.SaleOrderDetails");
            DropTable("PosCloud.ExpenseHeads");
            DropTable("PosCloud.Expenses");
            DropTable("PosCloud.Designations");
            DropTable("PosCloud.Departments");
            DropTable("PosCloud.Employees");
            DropTable("PosCloud.SaleOrders");
            DropTable("PosCloud.Customers");
            DropTable("PosCloud.States");
            DropTable("PosCloud.Cities");
        }
    }
}
