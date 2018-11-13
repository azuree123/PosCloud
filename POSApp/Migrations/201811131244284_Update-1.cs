namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppCounters",
                c => new
                    {
                        Id = c.Byte(nullable: false, identity: true),
                        InvoiceTransId = c.Int(nullable: false),
                        PurchaseTransId = c.Int(nullable: false),
                        Voucher = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        FiscalYearId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "PosCloud.Devices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        License = c.String(nullable: false, maxLength: 150, unicode: false),
                        DeviceCode = c.String(nullable: false, maxLength: 150, unicode: false),
                        AppVersion = c.String(nullable: false, maxLength: 150, unicode: false),
                        DownloadedDate = c.DateTime(),
                        Address = c.String(nullable: false, maxLength: 150, unicode: false),
                        Contact = c.String(nullable: false, maxLength: 150, unicode: false),
                        City = c.String(maxLength: 150, unicode: false),
                        State = c.String(maxLength: 150, unicode: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "PosCloud.DineTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        DineTableNumber = c.String(nullable: false, maxLength: 10, unicode: false),
                        FloorId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Floors", t => new { t.FloorId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => new { t.FloorId, t.StoreId });
            
            CreateTable(
                "PosCloud.Floors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        FloorNumber = c.String(nullable: false, maxLength: 10, unicode: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
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
                        Type = c.String(nullable: false, maxLength: 150, unicode: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        DiscountCode = c.String(nullable: false, maxLength: 150, unicode: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValidFrom = c.DateTime(nullable: false),
                        ValidTill = c.DateTime(nullable: false),
                        IsPercentage = c.Boolean(),
                        IsTaxable = c.Boolean(),
                        Days = c.String(nullable: false, maxLength: 150, unicode: false),
                        IsActive = c.Boolean(),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
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
                .ForeignKey("PosCloud.Departments", t => new { t.DepartmentId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => new { t.DepartmentId, t.StoreId });
            
            CreateTable(
                "PosCloud.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
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
                .ForeignKey("PosCloud.Employees", t => new { t.EmployeeId, t.StoreId }, cascadeDelete: true)
                .ForeignKey("PosCloud.ExpenseHeads", t => new { t.ExpenseHeadId, t.StoreId }, cascadeDelete: true)
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
                "PosCloud.ModifierOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        Cost = c.Double(nullable: false),
                        CostType = c.String(nullable: false, maxLength: 150, unicode: false),
                        Price = c.Double(nullable: false),
                        TaxId = c.Int(),
                        IsTaxable = c.Boolean(nullable: false),
                        ModifierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Modifier", t => new { t.ModifierId, t.StoreId }, cascadeDelete: true)
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .ForeignKey("PosCloud.Taxes", t => new { t.TaxId, t.StoreId })
                .Index(t => new { t.ModifierId, t.StoreId })
                .Index(t => new { t.TaxId, t.StoreId });
            
            CreateTable(
                "PosCloud.Modifier",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        Barcode = c.String(maxLength: 150, unicode: false),
                        Store_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .ForeignKey("PosCloud.Stores", t => t.Store_Id)
                .Index(t => t.StoreId)
                .Index(t => t.Store_Id);
            
            CreateTable(
                "PosCloud.Products",
                c => new
                    {
                        ProductCode = c.String(nullable: false, maxLength: 150, unicode: false),
                        StoreId = c.Int(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        Description = c.String(maxLength: 300, unicode: false),
                        Type = c.String(),
                        Attribute = c.String(maxLength: 150, unicode: false),
                        Size = c.String(maxLength: 150, unicode: false),
                        TaxId = c.Int(),
                        IsTaxable = c.Boolean(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        CostPrice = c.Double(nullable: false),
                        Stock = c.Double(),
                        ReOrderLevel = c.Int(nullable: false),
                        Barcode = c.String(maxLength: 150, unicode: false),
                        Image = c.Binary(),
                        CategoryId = c.Int(nullable: false),
                        UnitId = c.Int(nullable: false),
                        SectionId = c.Int(),
                        InventoryItem = c.Boolean(nullable: false),
                        PurchaseItem = c.Boolean(nullable: false),
                        FixedAssetItem = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => new { t.ProductCode, t.StoreId })
                .ForeignKey("PosCloud.ProductCategories", t => new { t.CategoryId, t.StoreId }, cascadeDelete: true)
                .ForeignKey("PosCloud.Units", t => new { t.UnitId, t.StoreId })
                .ForeignKey("PosCloud.Sections", t => new { t.SectionId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .ForeignKey("PosCloud.Taxes", t => new { t.TaxId, t.StoreId })
                .Index(t => new { t.CategoryId, t.StoreId })
                .Index(t => new { t.UnitId, t.StoreId })
                .Index(t => new { t.SectionId, t.StoreId })
                .Index(t => new { t.TaxId, t.StoreId });
            
            CreateTable(
                "PosCloud.ProductsSubs",
                c => new
                    {
                        ComboProductCode = c.String(nullable: false, maxLength: 150, unicode: false),
                        StoreId = c.Int(nullable: false),
                        ProductCode = c.String(nullable: false, maxLength: 150, unicode: false),
                        Qty = c.Double(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => new { t.ComboProductCode, t.StoreId, t.ProductCode })
                .ForeignKey("PosCloud.Products", t => new { t.ComboProductCode, t.StoreId })
                .ForeignKey("PosCloud.Products", t => new { t.ProductCode, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => new { t.ComboProductCode, t.StoreId })
                .Index(t => new { t.ProductCode, t.StoreId });
            
            CreateTable(
                "PosCloud.ProductCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        Image = c.Binary(),
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
                "PosCloud.Units",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        UnitCode = c.String(maxLength: 150, unicode: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "PosCloud.Sections",
                c => new
                    {
                        SectionId = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                    })
                .PrimaryKey(t => new { t.SectionId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "PosCloud.POSTerminals",
                c => new
                    {
                        POSTerminalId = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        IsActive = c.Boolean(),
                        SectionId = c.Int(),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                    })
                .PrimaryKey(t => new { t.POSTerminalId, t.StoreId })
                .ForeignKey("PosCloud.Sections", t => new { t.SectionId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => new { t.SectionId, t.StoreId });
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        EmployeeId = c.Int(),
                        StoreId = c.Int(),
                        POSTerminalId = c.Int(),
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
                .ForeignKey("PosCloud.POSTerminals", t => new { t.POSTerminalId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => new { t.POSTerminalId, t.StoreId })
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
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                "PosCloud.TimedEventProducts",
                c => new
                    {
                        ProductCode = c.String(nullable: false, maxLength: 150, unicode: false),
                        StoreId = c.Int(nullable: false),
                        TimedEventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductCode, t.StoreId, t.TimedEventId })
                .ForeignKey("PosCloud.Products", t => new { t.ProductCode, t.StoreId }, cascadeDelete: true)
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .ForeignKey("PosCloud.TimedEvents", t => new { t.TimedEventId, t.StoreId }, cascadeDelete: true)
                .Index(t => new { t.ProductCode, t.StoreId })
                .Index(t => new { t.TimedEventId, t.StoreId });
            
            CreateTable(
                "PosCloud.TimedEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        Type = c.String(nullable: false, maxLength: 150, unicode: false),
                        Value = c.Double(nullable: false),
                        FromDate = c.DateTime(nullable: false, storeType: "date"),
                        ToDate = c.DateTime(nullable: false, storeType: "date"),
                        FromHour = c.Time(nullable: false, precision: 7),
                        ToHour = c.Time(nullable: false, precision: 7),
                        Days = c.String(maxLength: 100, unicode: false),
                        IsActive = c.Boolean(nullable: false),
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
                        ProductCode = c.String(nullable: false, maxLength: 150, unicode: false),
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
                .ForeignKey("PosCloud.Products", t => new { t.ProductCode, t.StoreId }, cascadeDelete: true)
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .ForeignKey("PosCloud.TransMaster", t => new { t.TransMasterId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.Store_Id)
                .Index(t => new { t.ProductCode, t.StoreId })
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
                        DineTableId = c.Int(),
                        OrderTime = c.Int(),
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
                .ForeignKey("PosCloud.DineTables", t => new { t.DineTableId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => new { t.BusinessPartnerId, t.StoreId })
                .Index(t => new { t.DineTableId, t.StoreId })
                .Index(t => t.TransCode, unique: true);
            
            CreateTable(
                "PosCloud.ReportLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        Details = c.String(maxLength: 8000, unicode: false),
                        Path = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Status = c.String(maxLength: 150, unicode: false),
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
                "PosCloud.SecurityRights",
                c => new
                    {
                        IdentityUserRoleId = c.String(nullable: false, maxLength: 128),
                        StoreId = c.Int(nullable: false),
                        SecurityObjectId = c.Int(nullable: false),
                        Manage = c.Boolean(nullable: false),
                        View = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdentityUserRoleId, t.StoreId, t.SecurityObjectId })
                .ForeignKey("dbo.AspNetRoles", t => t.IdentityUserRoleId, cascadeDelete: true)
                .ForeignKey("PosCloud.SecurityObjects", t => t.SecurityObjectId, cascadeDelete: true)
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.IdentityUserRoleId)
                .Index(t => t.StoreId)
                .Index(t => t.SecurityObjectId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        StoreId = c.Int(),
                        CreatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex")
                .Index(t => t.StoreId);
            
            CreateTable(
                "PosCloud.SecurityObjects",
                c => new
                    {
                        SecurityObjectId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 150),
                        Type = c.String(maxLength: 150),
                        Module = c.String(maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.SecurityObjectId);
            
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
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
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
                "PosCloud.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(maxLength: 300),
                        Contact = c.String(maxLength: 150),
                        City = c.String(maxLength: 150),
                        State = c.String(maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
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
                "PosCloud.ProductCategoryGroups",
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
                "PosCloud.ProductModifiers",
                c => new
                    {
                        ProductCode = c.Int(nullable: false),
                        ProductStoreCode = c.Int(nullable: false),
                        ModifierId = c.String(nullable: false, maxLength: 150, unicode: false),
                        ModifierStoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductCode, t.ProductStoreCode, t.ModifierId, t.ModifierStoreId })
                .ForeignKey("PosCloud.Modifier", t => new { t.ProductCode, t.ProductStoreCode }, cascadeDelete: true)
                .ForeignKey("PosCloud.Products", t => new { t.ModifierId, t.ModifierStoreId }, cascadeDelete: true)
                .Index(t => new { t.ProductCode, t.ProductStoreCode })
                .Index(t => new { t.ModifierId, t.ModifierStoreId });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("PosCloud.ProductCategoryGroups", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Cities", "StateId", "PosCloud.States");
            DropForeignKey("PosCloud.BusinessPartners", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TransDetails", "Store_Id", "PosCloud.Stores");
            DropForeignKey("PosCloud.SecurityRights", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.SecurityRights", "SecurityObjectId", "PosCloud.SecurityObjects");
            DropForeignKey("PosCloud.SecurityRights", "IdentityUserRoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetRoles", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ReportLogs", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Modifier", "Store_Id", "PosCloud.Stores");
            DropForeignKey("PosCloud.ModifierOptions", new[] { "TaxId", "StoreId" }, "PosCloud.Taxes");
            DropForeignKey("PosCloud.ModifierOptions", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ModifierOptions", new[] { "ModifierId", "StoreId" }, "PosCloud.Modifier");
            DropForeignKey("PosCloud.Modifier", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ProductModifiers", new[] { "ModifierId", "ModifierStoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.ProductModifiers", new[] { "ProductCode", "ProductStoreCode" }, "PosCloud.Modifier");
            DropForeignKey("PosCloud.TransDetails", new[] { "TransMasterId", "StoreId" }, "PosCloud.TransMaster");
            DropForeignKey("PosCloud.TransMaster", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TransMaster", new[] { "DineTableId", "StoreId" }, "PosCloud.DineTables");
            DropForeignKey("PosCloud.TransMaster", new[] { "BusinessPartnerId", "StoreId" }, "PosCloud.BusinessPartners");
            DropForeignKey("PosCloud.TransDetails", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TransDetails", new[] { "ProductCode", "StoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.TimedEventProducts", new[] { "TimedEventId", "StoreId" }, "PosCloud.TimedEvents");
            DropForeignKey("PosCloud.TimedEvents", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TimedEventProducts", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TimedEventProducts", new[] { "ProductCode", "StoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.Products", new[] { "TaxId", "StoreId" }, "PosCloud.Taxes");
            DropForeignKey("PosCloud.Taxes", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Products", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Products", new[] { "SectionId", "StoreId" }, "PosCloud.Sections");
            DropForeignKey("PosCloud.Sections", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.POSTerminals", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.POSTerminals", new[] { "SectionId", "StoreId" }, "PosCloud.Sections");
            DropForeignKey("dbo.AspNetUsers", "StoreId", "PosCloud.Stores");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", new[] { "POSTerminalId", "StoreId" }, "PosCloud.POSTerminals");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", new[] { "Employee_Id", "Employee_StoreId" }, "PosCloud.Employees");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("PosCloud.Products", new[] { "UnitId", "StoreId" }, "PosCloud.Units");
            DropForeignKey("PosCloud.Units", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Products", new[] { "CategoryId", "StoreId" }, "PosCloud.ProductCategories");
            DropForeignKey("PosCloud.ProductCategories", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ProductsSubs", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ProductsSubs", new[] { "ProductCode", "StoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.ProductsSubs", new[] { "ComboProductCode", "StoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.Employees", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Expenses", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Expenses", new[] { "ExpenseHeadId", "StoreId" }, "PosCloud.ExpenseHeads");
            DropForeignKey("PosCloud.ExpenseHeads", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Expenses", new[] { "EmployeeId", "StoreId" }, "PosCloud.Employees");
            DropForeignKey("PosCloud.Employees", new[] { "DepartmentId", "StoreId" }, "PosCloud.Departments");
            DropForeignKey("PosCloud.Departments", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Discounts", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.DineTables", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.DineTables", new[] { "FloorId", "StoreId" }, "PosCloud.Floors");
            DropForeignKey("PosCloud.Floors", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Devices", "StoreId", "PosCloud.Stores");
            DropIndex("PosCloud.ProductModifiers", new[] { "ModifierId", "ModifierStoreId" });
            DropIndex("PosCloud.ProductModifiers", new[] { "ProductCode", "ProductStoreCode" });
            DropIndex("PosCloud.ProductCategoryGroups", new[] { "StoreId" });
            DropIndex("PosCloud.Cities", new[] { "StateId" });
            DropIndex("dbo.AspNetRoles", new[] { "StoreId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("PosCloud.SecurityRights", new[] { "SecurityObjectId" });
            DropIndex("PosCloud.SecurityRights", new[] { "StoreId" });
            DropIndex("PosCloud.SecurityRights", new[] { "IdentityUserRoleId" });
            DropIndex("PosCloud.ReportLogs", new[] { "StoreId" });
            DropIndex("PosCloud.TransMaster", new[] { "TransCode" });
            DropIndex("PosCloud.TransMaster", new[] { "DineTableId", "StoreId" });
            DropIndex("PosCloud.TransMaster", new[] { "BusinessPartnerId", "StoreId" });
            DropIndex("PosCloud.TransDetails", new[] { "Store_Id" });
            DropIndex("PosCloud.TransDetails", new[] { "TransMasterId", "StoreId" });
            DropIndex("PosCloud.TransDetails", new[] { "ProductCode", "StoreId" });
            DropIndex("PosCloud.TimedEvents", new[] { "StoreId" });
            DropIndex("PosCloud.TimedEventProducts", new[] { "TimedEventId", "StoreId" });
            DropIndex("PosCloud.TimedEventProducts", new[] { "ProductCode", "StoreId" });
            DropIndex("PosCloud.Taxes", new[] { "StoreId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Employee_Id", "Employee_StoreId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "POSTerminalId", "StoreId" });
            DropIndex("PosCloud.POSTerminals", new[] { "SectionId", "StoreId" });
            DropIndex("PosCloud.Sections", new[] { "StoreId" });
            DropIndex("PosCloud.Units", new[] { "StoreId" });
            DropIndex("PosCloud.ProductCategories", new[] { "StoreId" });
            DropIndex("PosCloud.ProductsSubs", new[] { "ProductCode", "StoreId" });
            DropIndex("PosCloud.ProductsSubs", new[] { "ComboProductCode", "StoreId" });
            DropIndex("PosCloud.Products", new[] { "TaxId", "StoreId" });
            DropIndex("PosCloud.Products", new[] { "SectionId", "StoreId" });
            DropIndex("PosCloud.Products", new[] { "UnitId", "StoreId" });
            DropIndex("PosCloud.Products", new[] { "CategoryId", "StoreId" });
            DropIndex("PosCloud.Modifier", new[] { "Store_Id" });
            DropIndex("PosCloud.Modifier", new[] { "StoreId" });
            DropIndex("PosCloud.ModifierOptions", new[] { "TaxId", "StoreId" });
            DropIndex("PosCloud.ModifierOptions", new[] { "ModifierId", "StoreId" });
            DropIndex("PosCloud.ExpenseHeads", new[] { "StoreId" });
            DropIndex("PosCloud.Expenses", new[] { "ExpenseHeadId", "StoreId" });
            DropIndex("PosCloud.Expenses", new[] { "EmployeeId", "StoreId" });
            DropIndex("PosCloud.Departments", new[] { "StoreId" });
            DropIndex("PosCloud.Employees", new[] { "DepartmentId", "StoreId" });
            DropIndex("PosCloud.Discounts", new[] { "StoreId" });
            DropIndex("PosCloud.Floors", new[] { "StoreId" });
            DropIndex("PosCloud.DineTables", new[] { "FloorId", "StoreId" });
            DropIndex("PosCloud.Devices", new[] { "StoreId" });
            DropIndex("PosCloud.BusinessPartners", new[] { "StoreId" });
            DropTable("PosCloud.ProductModifiers");
            DropTable("PosCloud.ProductCategoryGroups");
            DropTable("PosCloud.Locations");
            DropTable("PosCloud.Clients");
            DropTable("PosCloud.States");
            DropTable("PosCloud.Cities");
            DropTable("PosCloud.SecurityObjects");
            DropTable("dbo.AspNetRoles");
            DropTable("PosCloud.SecurityRights");
            DropTable("PosCloud.ReportLogs");
            DropTable("PosCloud.TransMaster");
            DropTable("PosCloud.TransDetails");
            DropTable("PosCloud.TimedEvents");
            DropTable("PosCloud.TimedEventProducts");
            DropTable("PosCloud.Taxes");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("PosCloud.POSTerminals");
            DropTable("PosCloud.Sections");
            DropTable("PosCloud.Units");
            DropTable("PosCloud.ProductCategories");
            DropTable("PosCloud.ProductsSubs");
            DropTable("PosCloud.Products");
            DropTable("PosCloud.Modifier");
            DropTable("PosCloud.ModifierOptions");
            DropTable("PosCloud.ExpenseHeads");
            DropTable("PosCloud.Expenses");
            DropTable("PosCloud.Departments");
            DropTable("PosCloud.Employees");
            DropTable("PosCloud.Discounts");
            DropTable("PosCloud.Floors");
            DropTable("PosCloud.DineTables");
            DropTable("PosCloud.Devices");
            DropTable("PosCloud.Stores");
            DropTable("PosCloud.BusinessPartners");
            DropTable("dbo.AppCounters");
        }
    }
}
