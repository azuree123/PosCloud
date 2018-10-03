namespace PoSCloudApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init1 : DbMigration
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
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 150),
                        UpdatedOn = c.DateTime(nullable: false),
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
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 150),
                        UpdatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 150),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Referral = c.String(),
                        Gender = c.String(),
                        Address = c.String(),
                        State = c.String(),
                        City = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        Note = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        MobileNumber = c.String(),
                        Salary = c.Double(nullable: false),
                        Commission = c.Double(nullable: false),
                        JoinDate = c.DateTime(nullable: false),
                        Booking = c.Boolean(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        DesignationId = c.Int(nullable: false),
                        Address = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExpenseHeads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Details = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExpenseHead = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        Description = c.String(),
                        PurchaseId = c.Int(nullable: false),
                        VoucherId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Contact = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.String(),
                        Type = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProductCode = c.String(),
                        CategoryId = c.Int(nullable: false),
                        Duration = c.String(),
                        Available = c.String(),
                        SupplierId = c.Int(nullable: false),
                        Tax = c.Double(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        Stock = c.String(),
                        Barcode = c.String(),
                        Image = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PurchaseOderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PurchaseOrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        RetailPrice = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PurchaseOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SupplierId = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        SupplyDate = c.DateTime(nullable: false),
                        InvoiceId = c.Int(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                        Type = c.String(),
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
                "dbo.SaleOrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SaleOrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SaleOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        CustomerId = c.Int(nullable: false),
                        Date = c.String(),
                        Time = c.String(),
                        Amount = c.Double(nullable: false),
                        CashierId = c.Int(nullable: false),
                        Tax = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                        Status = c.String(),
                        Canceled = c.Boolean(nullable: false),
                        Type = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Address = c.String(),
                        Company = c.String(),
                        State = c.String(),
                        City = c.String(),
                        Balance = c.Double(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            DropForeignKey("PosCloud.Cities", "StateId", "PosCloud.States");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("PosCloud.Cities", new[] { "StateId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Suppliers");
            DropTable("dbo.SaleOrders");
            DropTable("dbo.SaleOrderDetails");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PurchaseOrders");
            DropTable("dbo.PurchaseOderDetails");
            DropTable("dbo.Products");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Locations");
            DropTable("dbo.Expenses");
            DropTable("dbo.ExpenseHeads");
            DropTable("dbo.Employees");
            DropTable("dbo.Designations");
            DropTable("dbo.Departments");
            DropTable("dbo.Customers");
            DropTable("PosCloud.States");
            DropTable("PosCloud.Cities");
        }
    }
}
