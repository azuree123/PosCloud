namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "PosCloud.BusinessPartners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Type = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        PhoneNumber = c.String(maxLength: 150, unicode: false),
                        Email = c.String(maxLength: 150, unicode: false),
                        Address = c.String(maxLength: 150, unicode: false),
                        State = c.String(maxLength: 150, unicode: false),
                        City = c.String(maxLength: 150, unicode: false),
                        ContactPerson = c.String(maxLength: 150, unicode: false),
                        CpMobileNumber = c.String(maxLength: 150, unicode: false),
                        Birthday = c.DateTime(),
                        Remarks = c.String(maxLength: 250, unicode: false),
                        CreatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "PosCloud.Stores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        Address = c.String(maxLength: 150, unicode: false),
                        Contact = c.String(maxLength: 150, unicode: false),
                        City = c.String(maxLength: 150, unicode: false),
                        State = c.String(maxLength: 150, unicode: false),
                        IsOperational = c.Boolean(),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "PosCloud.Coupons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        Value = c.Double(nullable: false),
                        ValidFrom = c.DateTime(nullable: false, storeType: "date"),
                        ValidTill = c.DateTime(nullable: false, storeType: "date"),
                        Amount = c.Int(nullable: false),
                        Days = c.String(maxLength: 150, unicode: false),
                        IsPercentage = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "PosCloud.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        PhoneNumber = c.String(maxLength: 150, unicode: false),
                        Email = c.String(maxLength: 150, unicode: false),
                        Address = c.String(maxLength: 150, unicode: false),
                        State = c.String(maxLength: 150, unicode: false),
                        City = c.String(maxLength: 150, unicode: false),
                        Birthday = c.DateTime(),
                        Note = c.String(maxLength: 150, unicode: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "PosCloud.SaleOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Date = c.String(maxLength: 150, unicode: false),
                        Time = c.String(maxLength: 8000, unicode: false),
                        Amount = c.Double(nullable: false),
                        Tax = c.Double(),
                        Discount = c.Double(),
                        Status = c.String(maxLength: 50, unicode: false),
                        Canceled = c.Boolean(),
                        Type = c.String(maxLength: 150, unicode: false),
                        CustomerId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                        Employee_Id = c.Int(),
                        Employee_StoreId = c.Int(),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Customers", t => new { t.CustomerId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .ForeignKey("PosCloud.Employees", t => new { t.Employee_Id, t.Employee_StoreId })
                .Index(t => new { t.CustomerId, t.StoreId })
                .Index(t => new { t.Employee_Id, t.Employee_StoreId });
            
            CreateTable(
                "PosCloud.SaleOrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        SaleOrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(),
                        UnitPrice = c.Decimal(precision: 18, scale: 2),
                        Discount = c.Double(),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Products", t => new { t.ProductId, t.StoreId })
                .ForeignKey("PosCloud.SaleOrders", t => new { t.SaleOrderId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => new { t.ProductId, t.StoreId })
                .Index(t => new { t.SaleOrderId, t.StoreId });
            
            CreateTable(
                "PosCloud.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        Description = c.String(),
                        Type = c.String(),
                        ProductCode = c.String(),
                        Attribute = c.String(),
                        Size = c.String(),
                        Tax = c.Double(),
                        UnitPrice = c.Double(),
                        CostPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Stock = c.Double(),
                        ReOrderLevel = c.Int(nullable: false),
                        Barcode = c.String(maxLength: 150),
                        Image = c.String(maxLength: 150),
                        CategoryId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.ProductCategories", t => new { t.CategoryId, t.StoreId }, cascadeDelete: true)
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => new { t.CategoryId, t.StoreId });
            
            CreateTable(
                "PosCloud.ProductCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        Image = c.String(maxLength: 150),
                        Type = c.String(maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "PosCloud.Discounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        Amount = c.Double(nullable: false),
                        IsPercentage = c.Boolean(nullable: false),
                        IsTaxable = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "PosCloud.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        Email = c.String(maxLength: 150, unicode: false),
                        Gender = c.String(maxLength: 150, unicode: false),
                        MobileNumber = c.String(maxLength: 150, unicode: false),
                        Salary = c.Double(),
                        Commission = c.Double(),
                        JoinDate = c.DateTime(),
                        DepartmentId = c.Int(nullable: false),
                        DesignationId = c.Int(nullable: false),
                        Address = c.String(maxLength: 150, unicode: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Departments", t => t.DepartmentId)
                .ForeignKey("PosCloud.Designations", t => t.DesignationId)
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId)
                .Index(t => t.DepartmentId)
                .Index(t => t.DesignationId);
            
            CreateTable(
                "PosCloud.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "PosCloud.Designations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "PosCloud.Expenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        ExpenseHeadId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        Amount = c.Double(),
                        Description = c.String(maxLength: 150),
                        Date = c.DateTime(),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Employees", t => new { t.EmployeeId, t.StoreId })
                .ForeignKey("PosCloud.ExpenseHeads", t => new { t.ExpenseHeadId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => new { t.EmployeeId, t.StoreId })
                .Index(t => new { t.ExpenseHeadId, t.StoreId });
            
            CreateTable(
                "PosCloud.ExpenseHeads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        Details = c.String(maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "PosCloud.PurchaseOrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        PurchaseOrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(),
                        Discount = c.Double(),
                        UnitPrice = c.Decimal(precision: 18, scale: 2),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Products", t => new { t.ProductId, t.StoreId })
                .ForeignKey("PosCloud.PurchaseOrders", t => new { t.PurchaseOrderId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => new { t.ProductId, t.StoreId })
                .Index(t => new { t.PurchaseOrderId, t.StoreId });
            
            CreateTable(
                "PosCloud.PurchaseOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        OrderDate = c.DateTime(),
                        SupplyDate = c.DateTime(),
                        InvoiceId = c.Int(nullable: false),
                        TotalPrice = c.Double(),
                        Type = c.String(maxLength: 8000, unicode: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .ForeignKey("PosCloud.Suppliers", t => new { t.SupplierId, t.StoreId })
                .Index(t => t.StoreId)
                .Index(t => new { t.SupplierId, t.StoreId });
            
            CreateTable(
                "PosCloud.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        PhoneNumber = c.String(maxLength: 150),
                        ContactPerson = c.String(),
                        CpMobileNumber = c.String(),
                        Email = c.String(maxLength: 150),
                        Address = c.String(maxLength: 4000),
                        State = c.String(maxLength: 50),
                        City = c.String(maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "PosCloud.Taxes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        Rate = c.Double(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "PosCloud.TransDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        TransMasterId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(precision: 18, scale: 2),
                        CreatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                        Store_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Products", t => new { t.ProductId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .ForeignKey("PosCloud.TransMaster", t => new { t.TransMasterId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.Store_Id)
                .Index(t => new { t.ProductId, t.StoreId })
                .Index(t => new { t.TransMasterId, t.StoreId })
                .Index(t => t.Store_Id);
            
            CreateTable(
                "PosCloud.TransMaster",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Type = c.String(maxLength: 3, fixedLength: true, unicode: false),
                        TransCode = c.String(maxLength: 25, unicode: false),
                        BusinessPartnerId = c.Int(nullable: false),
                        TransDate = c.DateTime(nullable: false),
                        TransRef = c.String(maxLength: 25, unicode: false),
                        TransStatus = c.String(maxLength: 25, unicode: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Posted = c.Boolean(nullable: false),
                        ACRef = c.String(maxLength: 25, unicode: false),
                        PaymentMethod = c.String(),
                        CreatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.BusinessPartners", t => new { t.BusinessPartnerId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => new { t.BusinessPartnerId, t.StoreId })
                .Index(t => t.TransCode, unique: true);
            
            CreateTable(
                "PosCloud.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        StateId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("PosCloud.States", t => t.StateId)
                .Index(t => t.StateId);
            
            CreateTable(
                "PosCloud.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
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
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductCategoryGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StoreId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
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
                        EmployeeId = c.Int(),
                        StoreId = c.Int(),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
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
                        Employee_Id = c.Int(),
                        Employee_StoreId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("PosCloud.Employees", t => new { t.Employee_Id, t.Employee_StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => new { t.Employee_Id, t.Employee_StoreId });
            
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
            DropForeignKey("dbo.AspNetUsers", "StoreId", "PosCloud.Stores");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", new[] { "Employee_Id", "Employee_StoreId" }, "PosCloud.Employees");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ProductCategoryGroups", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Cities", "StateId", "PosCloud.States");
            DropForeignKey("PosCloud.BusinessPartners", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TransDetails", "Store_Id", "PosCloud.Stores");
            DropForeignKey("PosCloud.TransDetails", new[] { "TransMasterId", "StoreId" }, "PosCloud.TransMaster");
            DropForeignKey("PosCloud.TransMaster", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TransMaster", new[] { "BusinessPartnerId", "StoreId" }, "PosCloud.BusinessPartners");
            DropForeignKey("PosCloud.TransDetails", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TransDetails", new[] { "ProductId", "StoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.Taxes", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.PurchaseOrderDetails", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.PurchaseOrderDetails", new[] { "PurchaseOrderId", "StoreId" }, "PosCloud.PurchaseOrders");
            DropForeignKey("PosCloud.PurchaseOrders", new[] { "SupplierId", "StoreId" }, "PosCloud.Suppliers");
            DropForeignKey("PosCloud.Suppliers", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.PurchaseOrders", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.PurchaseOrderDetails", new[] { "ProductId", "StoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.Employees", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.SaleOrders", new[] { "Employee_Id", "Employee_StoreId" }, "PosCloud.Employees");
            DropForeignKey("PosCloud.Expenses", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Expenses", new[] { "ExpenseHeadId", "StoreId" }, "PosCloud.ExpenseHeads");
            DropForeignKey("PosCloud.ExpenseHeads", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Expenses", new[] { "EmployeeId", "StoreId" }, "PosCloud.Employees");
            DropForeignKey("PosCloud.Employees", "DesignationId", "PosCloud.Designations");
            DropForeignKey("PosCloud.Employees", "DepartmentId", "PosCloud.Departments");
            DropForeignKey("PosCloud.Discounts", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Customers", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.SaleOrders", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.SaleOrderDetails", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.SaleOrderDetails", new[] { "SaleOrderId", "StoreId" }, "PosCloud.SaleOrders");
            DropForeignKey("PosCloud.SaleOrderDetails", new[] { "ProductId", "StoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.Products", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Products", new[] { "CategoryId", "StoreId" }, "PosCloud.ProductCategories");
            DropForeignKey("PosCloud.ProductCategories", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.SaleOrders", new[] { "CustomerId", "StoreId" }, "PosCloud.Customers");
            DropForeignKey("PosCloud.Coupons", "StoreId", "PosCloud.Stores");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Employee_Id", "Employee_StoreId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "StoreId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ProductCategoryGroups", new[] { "StoreId" });
            DropIndex("PosCloud.Cities", new[] { "StateId" });
            DropIndex("PosCloud.TransMaster", new[] { "TransCode" });
            DropIndex("PosCloud.TransMaster", new[] { "BusinessPartnerId", "StoreId" });
            DropIndex("PosCloud.TransDetails", new[] { "Store_Id" });
            DropIndex("PosCloud.TransDetails", new[] { "TransMasterId", "StoreId" });
            DropIndex("PosCloud.TransDetails", new[] { "ProductId", "StoreId" });
            DropIndex("PosCloud.Taxes", new[] { "StoreId" });
            DropIndex("PosCloud.Suppliers", new[] { "StoreId" });
            DropIndex("PosCloud.PurchaseOrders", new[] { "SupplierId", "StoreId" });
            DropIndex("PosCloud.PurchaseOrders", new[] { "StoreId" });
            DropIndex("PosCloud.PurchaseOrderDetails", new[] { "PurchaseOrderId", "StoreId" });
            DropIndex("PosCloud.PurchaseOrderDetails", new[] { "ProductId", "StoreId" });
            DropIndex("PosCloud.ExpenseHeads", new[] { "StoreId" });
            DropIndex("PosCloud.Expenses", new[] { "ExpenseHeadId", "StoreId" });
            DropIndex("PosCloud.Expenses", new[] { "EmployeeId", "StoreId" });
            DropIndex("PosCloud.Employees", new[] { "DesignationId" });
            DropIndex("PosCloud.Employees", new[] { "DepartmentId" });
            DropIndex("PosCloud.Employees", new[] { "StoreId" });
            DropIndex("PosCloud.Discounts", new[] { "StoreId" });
            DropIndex("PosCloud.ProductCategories", new[] { "StoreId" });
            DropIndex("PosCloud.Products", new[] { "CategoryId", "StoreId" });
            DropIndex("PosCloud.SaleOrderDetails", new[] { "SaleOrderId", "StoreId" });
            DropIndex("PosCloud.SaleOrderDetails", new[] { "ProductId", "StoreId" });
            DropIndex("PosCloud.SaleOrders", new[] { "Employee_Id", "Employee_StoreId" });
            DropIndex("PosCloud.SaleOrders", new[] { "CustomerId", "StoreId" });
            DropIndex("PosCloud.Customers", new[] { "StoreId" });
            DropIndex("PosCloud.Coupons", new[] { "StoreId" });
            DropIndex("PosCloud.BusinessPartners", new[] { "StoreId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ProductCategoryGroups");
            DropTable("PosCloud.Locations");
            DropTable("PosCloud.States");
            DropTable("PosCloud.Cities");
            DropTable("PosCloud.TransMaster");
            DropTable("PosCloud.TransDetails");
            DropTable("PosCloud.Taxes");
            DropTable("PosCloud.Suppliers");
            DropTable("PosCloud.PurchaseOrders");
            DropTable("PosCloud.PurchaseOrderDetails");
            DropTable("PosCloud.ExpenseHeads");
            DropTable("PosCloud.Expenses");
            DropTable("PosCloud.Designations");
            DropTable("PosCloud.Departments");
            DropTable("PosCloud.Employees");
            DropTable("PosCloud.Discounts");
            DropTable("PosCloud.ProductCategories");
            DropTable("PosCloud.Products");
            DropTable("PosCloud.SaleOrderDetails");
            DropTable("PosCloud.SaleOrders");
            DropTable("PosCloud.Customers");
            DropTable("PosCloud.Coupons");
            DropTable("PosCloud.Stores");
            DropTable("PosCloud.BusinessPartners");
        }
    }
}
