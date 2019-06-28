namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "PosCloud.AppCounters",
                c => new
                    {
                        Id = c.Byte(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        DeviceId = c.Int(),
                        InvoiceTransId = c.Int(nullable: false),
                        HoldInvoiceTransId = c.Int(nullable: false),
                        PurchaseTransId = c.Int(nullable: false),
                        Voucher = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        FiscalYearId = c.Int(nullable: false),
                        MifId = c.Int(nullable: false),
                        STId = c.Int(nullable: false),
                        TransferId = c.Int(nullable: false),
                        PurchasingId = c.Int(nullable: false),
                        OtherInId = c.Int(nullable: false),
                        OtherOutId = c.Int(nullable: false),
                        ExpiryId = c.Int(nullable: false),
                        DamageId = c.Int(nullable: false),
                        WasteId = c.Int(nullable: false),
                        OpeningStockId = c.Int(nullable: false),
                        StockTakingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Devices", t => new { t.DeviceId, t.StoreId }, cascadeDelete: true)
                .ForeignKey("PosCloud.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => new { t.DeviceId, t.StoreId });
            
            CreateTable(
                "PosCloud.Devices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        ArabicName = c.String(maxLength: 150),
                        License = c.String(nullable: false, maxLength: 150, unicode: false),
                        DeviceCode = c.String(nullable: false, maxLength: 150, unicode: false),
                        AppVersion = c.String(nullable: false, maxLength: 150, unicode: false),
                        DownloadedDate = c.DateTime(),
                        ReceiptHeader = c.String(nullable: false, maxLength: 150, unicode: false),
                        ReceiptFooter = c.String(nullable: false, maxLength: 150, unicode: false),
                        RefundPin = c.String(nullable: false, maxLength: 8000, unicode: false),
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
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "PosCloud.IncrementalSyncronizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        DeviceId = c.Int(nullable: false),
                        LastSynced = c.DateTime(nullable: false),
                        TableName = c.String(nullable: false, maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId, t.DeviceId })
                .ForeignKey("PosCloud.Devices", t => new { t.DeviceId, t.StoreId }, cascadeDelete: true)
                .ForeignKey("PosCloud.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => new { t.DeviceId, t.StoreId });
            
            CreateTable(
                "PosCloud.Stores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        ArabicName = c.String(maxLength: 150),
                        Address = c.String(maxLength: 150, unicode: false),
                        Contact = c.String(maxLength: 150, unicode: false),
                        BusinessStartTime = c.DateTime(nullable: false),
                        Currency = c.String(maxLength: 150, unicode: false),
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
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("PosCloud.Clients", t => t.ClientId)
                .Index(t => t.ClientId);
            
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
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SecurityObjectId);
            
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
                "PosCloud.BusinessPartners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Type = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        ArabicName = c.String(maxLength: 150),
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
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "PosCloud.TransMaster",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        DeviceId = c.Int(),
                        TransferTo = c.String(maxLength: 25, unicode: false),
                        SessionCode = c.Int(nullable: false),
                        Type = c.String(maxLength: 3, fixedLength: true, unicode: false),
                        TransCode = c.String(maxLength: 25, unicode: false),
                        DeliveryType = c.String(maxLength: 25, unicode: false),
                        Address = c.String(maxLength: 150, unicode: false),
                        ContactNumber = c.String(maxLength: 25, unicode: false),
                        Name = c.String(maxLength: 25, unicode: false),
                        BusinessPartnerId = c.Int(),
                        WarehouseId = c.Int(),
                        TransDate = c.DateTime(nullable: false),
                        Tax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransRef = c.String(maxLength: 25, unicode: false),
                        TransStatus = c.String(maxLength: 25, unicode: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Posted = c.Boolean(nullable: false),
                        ACRef = c.String(maxLength: 25, unicode: false),
                        Description = c.String(),
                        PaymentMethod = c.String(),
                        Issued = c.Boolean(nullable: false),
                        DineTableId = c.Int(),
                        OrderTime = c.Int(),
                        DiscountId = c.Int(),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.BusinessPartners", t => new { t.BusinessPartnerId, t.StoreId })
                .ForeignKey("PosCloud.Devices", t => new { t.DeviceId, t.StoreId })
                .ForeignKey("PosCloud.DineTables", t => new { t.DineTableId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .ForeignKey("PosCloud.TimedEvents", t => new { t.DiscountId, t.StoreId }, cascadeDelete: true)
                .ForeignKey("PosCloud.Warehouses", t => t.WarehouseId)
                .Index(t => new { t.BusinessPartnerId, t.StoreId })
                .Index(t => new { t.DeviceId, t.StoreId })
                .Index(t => new { t.DineTableId, t.StoreId })
                .Index(t => new { t.DiscountId, t.StoreId })
                .Index(t => t.TransCode, unique: true)
                .Index(t => t.WarehouseId);
            
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
                        IsDisabled = c.Boolean(nullable: false),
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
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "PosCloud.TimedEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        ArabicName = c.String(maxLength: 150),
                        Type = c.String(nullable: false, maxLength: 150, unicode: false),
                        DiscountCode = c.String(nullable: false, maxLength: 7, unicode: false),
                        Value = c.Double(nullable: false),
                        FromDate = c.DateTime(nullable: false, storeType: "date"),
                        ToDate = c.DateTime(nullable: false, storeType: "date"),
                        FromHour = c.Time(nullable: false, precision: 7),
                        ToHour = c.Time(nullable: false, precision: 7),
                        Days = c.String(maxLength: 100, unicode: false),
                        IsActive = c.Boolean(nullable: false),
                        IsPercentage = c.Boolean(nullable: false),
                        IsTaxable = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                        IsDisabled = c.Boolean(nullable: false),
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
                "PosCloud.Products",
                c => new
                    {
                        ProductCode = c.String(nullable: false, maxLength: 150, unicode: false),
                        StoreId = c.Int(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        ArabicName = c.String(maxLength: 150),
                        Description = c.String(maxLength: 300, unicode: false),
                        Type = c.String(),
                        Attribute = c.String(maxLength: 150, unicode: false),
                        Size = c.String(maxLength: 150, unicode: false),
                        TaxId = c.Int(),
                        IsTaxable = c.Boolean(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        CostPrice = c.Double(),
                        Stock = c.Double(),
                        ReOrderLevel = c.Int(nullable: false),
                        Barcode = c.String(maxLength: 150, unicode: false),
                        Image = c.Binary(),
                        CategoryId = c.Int(),
                        UnitId = c.Int(),
                        SectionId = c.Int(),
                        InventoryItem = c.Boolean(nullable: false),
                        PurchaseItem = c.Boolean(nullable: false),
                        FixedAssetItem = c.Boolean(nullable: false),
                        PurchaseUnit = c.String(maxLength: 300, unicode: false),
                        StorageUnit = c.String(maxLength: 300, unicode: false),
                        IngredientUnit = c.String(maxLength: 300, unicode: false),
                        PtoSFactor = c.Decimal(precision: 18, scale: 2),
                        StoIFactor = c.Decimal(precision: 18, scale: 2),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                        IsDisabled = c.Boolean(nullable: false),
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
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Modifiable = c.Boolean(),
                        Qty = c.Double(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ComboProductCode, t.StoreId, t.ProductCode })
                .ForeignKey("PosCloud.Products", t => new { t.ComboProductCode, t.StoreId })
                .ForeignKey("PosCloud.Products", t => new { t.ProductCode, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => new { t.ComboProductCode, t.StoreId })
                .Index(t => new { t.ProductCode, t.StoreId });
            
            CreateTable(
                "dbo.ComboProductsTransDetails",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        StoreId = c.Int(nullable: false),
                        TransDetailId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductSubId = c.Int(nullable: false),
                        ProductsSub_ComboProductCode = c.String(maxLength: 150, unicode: false),
                        ProductsSub_StoreId = c.Int(),
                        ProductsSub_ProductCode = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("PosCloud.ProductsSubs", t => new { t.ProductsSub_ComboProductCode, t.ProductsSub_StoreId, t.ProductsSub_ProductCode })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .ForeignKey("PosCloud.TransDetails", t => new { t.Id, t.StoreId })
                .Index(t => new { t.Id, t.StoreId })
                .Index(t => t.StoreId)
                .Index(t => new { t.ProductsSub_ComboProductCode, t.ProductsSub_StoreId, t.ProductsSub_ProductCode });
            
            CreateTable(
                "PosCloud.TransDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Tax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransMasterId = c.Int(nullable: false),
                        ProductCode = c.String(nullable: false, maxLength: 150, unicode: false),
                        EmployeeId = c.Int(),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountId = c.Int(),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BatchNumber = c.String(maxLength: 4000),
                        ExpiryDate = c.DateTime(),
                        ManufactureDate = c.DateTime(),
                        Waste = c.Boolean(),
                        Discount = c.Decimal(precision: 18, scale: 2),
                        CreatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                        IsDisabled = c.Boolean(nullable: false),
                        Store_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Employees", t => new { t.EmployeeId, t.StoreId }, cascadeDelete: true)
                .ForeignKey("PosCloud.Products", t => new { t.ProductCode, t.StoreId }, cascadeDelete: true)
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .ForeignKey("PosCloud.TimedEvents", t => new { t.DiscountId, t.StoreId }, cascadeDelete: true)
                .ForeignKey("PosCloud.TransMaster", t => new { t.TransMasterId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.Store_Id)
                .Index(t => new { t.EmployeeId, t.StoreId })
                .Index(t => new { t.ProductCode, t.StoreId })
                .Index(t => new { t.DiscountId, t.StoreId })
                .Index(t => new { t.TransMasterId, t.StoreId })
                .Index(t => t.Store_Id);
            
            CreateTable(
                "PosCloud.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        ShiftId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        ArabicName = c.String(maxLength: 150),
                        Email = c.String(maxLength: 150, unicode: false),
                        Gender = c.String(maxLength: 150, unicode: false),
                        MobileNumber = c.String(maxLength: 150, unicode: false),
                        Salary = c.Double(),
                        Commission = c.Double(),
                        JoinDate = c.DateTime(),
                        DepartmentId = c.Int(nullable: false),
                        DesignationId = c.Int(nullable: false),
                        Address = c.String(maxLength: 150, unicode: false),
                        Image = c.Binary(),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Departments", t => new { t.DepartmentId, t.StoreId })
                .ForeignKey("PosCloud.Designations", t => new { t.DesignationId, t.StoreId })
                .ForeignKey("PosCloud.Shifts", t => new { t.ShiftId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => new { t.DepartmentId, t.StoreId })
                .Index(t => new { t.DesignationId, t.StoreId })
                .Index(t => new { t.ShiftId, t.StoreId });
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PasswordEncrypt = c.String(maxLength: 150, unicode: false),
                        EmployeeId = c.Int(nullable: false),
                        StoreId = c.Int(nullable: false),
                        POSTerminalId = c.Int(),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsDisabled = c.Boolean(nullable: false),
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
                        Shift_ShiftId = c.Int(),
                        Shift_StoreId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("PosCloud.Employees", t => new { t.EmployeeId, t.StoreId })
                .ForeignKey("PosCloud.POSTerminals", t => new { t.POSTerminalId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .ForeignKey("PosCloud.Shifts", t => new { t.Shift_ShiftId, t.Shift_StoreId })
                .Index(t => new { t.EmployeeId, t.StoreId })
                .Index(t => new { t.POSTerminalId, t.StoreId })
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => new { t.Shift_ShiftId, t.Shift_StoreId });
            
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
                "PosCloud.POSTerminals",
                c => new
                    {
                        POSTerminalId = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        ArabicName = c.String(maxLength: 150),
                        SectionId = c.Int(),
                        IsActive = c.Boolean(),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.POSTerminalId, t.StoreId })
                .ForeignKey("PosCloud.Sections", t => new { t.SectionId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => new { t.SectionId, t.StoreId });
            
            CreateTable(
                "PosCloud.Sections",
                c => new
                    {
                        SectionId = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        ArabicName = c.String(maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.SectionId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "PosCloud.TillOperations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        SessionCode = c.Int(nullable: false),
                        OperationDate = c.DateTime(nullable: false),
                        Remarks = c.String(maxLength: 150, unicode: false),
                        OpeningAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SystemAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PhysicalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CarryOut = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AdjustedCashAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AdjustedCreditAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AdjustedCreditNoteAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        ShiftId = c.Int(),
                        Status = c.Boolean(),
                        TillOperationType = c.String(maxLength: 150, unicode: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("PosCloud.Shifts", t => new { t.ShiftId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => new { t.ShiftId, t.StoreId })
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "PosCloud.Shifts",
                c => new
                    {
                        ShiftId = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 10, unicode: false),
                        ArabicName = c.String(maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShiftId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "PosCloud.UserStores",
                c => new
                    {
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        StoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUserId, t.StoreId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("PosCloud.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "PosCloud.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        ArabicName = c.String(maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "PosCloud.Designations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        ArabicName = c.String(maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                        IsDisabled = c.Boolean(nullable: false),
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
                        IsDisabled = c.Boolean(nullable: false),
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
                        ArabicName = c.String(maxLength: 150),
                        Details = c.String(maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "PosCloud.ModifierTransDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        ComboSubItem = c.Int(),
                        TransDetailId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ModifierOptionId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.ModifierOptions", t => new { t.ModifierOptionId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .ForeignKey("PosCloud.TransDetails", t => new { t.TransDetailId, t.StoreId })
                .Index(t => new { t.ModifierOptionId, t.StoreId })
                .Index(t => new { t.TransDetailId, t.StoreId });
            
            CreateTable(
                "PosCloud.ModifierOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        ArabicName = c.String(maxLength: 150),
                        Cost = c.Double(nullable: false),
                        CostType = c.String(nullable: false, maxLength: 150, unicode: false),
                        Price = c.Double(nullable: false),
                        TaxId = c.Int(),
                        IsTaxable = c.Boolean(nullable: false),
                        ModifierId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                        IsDisabled = c.Boolean(nullable: false),
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
                        ArabicName = c.String(maxLength: 150),
                        Barcode = c.String(maxLength: 150, unicode: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                        IsDisabled = c.Boolean(nullable: false),
                        Store_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .ForeignKey("PosCloud.Stores", t => t.Store_Id)
                .Index(t => t.StoreId)
                .Index(t => t.Store_Id);
            
            CreateTable(
                "PosCloud.ModifierLinkProducts",
                c => new
                    {
                        ModifierId = c.Int(nullable: false),
                        ProductCode = c.String(nullable: false, maxLength: 150, unicode: false),
                        ProductStoreId = c.Int(nullable: false),
                        ModifierStoreId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                        IsDisabled = c.Boolean(nullable: false),
                        Store_Id = c.Int(),
                        Store_Id1 = c.Int(),
                    })
                .PrimaryKey(t => new { t.ModifierId, t.ProductCode })
                .ForeignKey("PosCloud.Modifier", t => new { t.ModifierId, t.ModifierStoreId }, cascadeDelete: true)
                .ForeignKey("PosCloud.Stores", t => t.ModifierStoreId)
                .ForeignKey("PosCloud.Products", t => new { t.ProductCode, t.ProductStoreId }, cascadeDelete: true)
                .ForeignKey("PosCloud.Stores", t => t.ProductStoreId)
                .ForeignKey("PosCloud.Stores", t => t.Store_Id)
                .ForeignKey("PosCloud.Stores", t => t.Store_Id1)
                .Index(t => new { t.ModifierId, t.ModifierStoreId })
                .Index(t => new { t.ProductCode, t.ProductStoreId })
                .Index(t => t.Store_Id)
                .Index(t => t.Store_Id1);
            
            CreateTable(
                "PosCloud.Taxes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        ArabicName = c.String(maxLength: 150),
                        Rate = c.Double(nullable: false),
                        IsPercentage = c.Boolean(),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "PosCloud.Recipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IngredientCode = c.String(nullable: false, maxLength: 150, unicode: false),
                        StoreId = c.Int(nullable: false),
                        ProductCode = c.String(nullable: false, maxLength: 150, unicode: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Calories = c.Decimal(precision: 18, scale: 2),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.IngredientCode, t.StoreId, t.ProductCode })
                .ForeignKey("PosCloud.Products", t => new { t.IngredientCode, t.StoreId })
                .ForeignKey("PosCloud.Products", t => new { t.ProductCode, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => new { t.IngredientCode, t.StoreId })
                .Index(t => new { t.ProductCode, t.StoreId });
            
            CreateTable(
                "PosCloud.ProductCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        ArabicName = c.String(maxLength: 150),
                        Image = c.Binary(),
                        Type = c.String(maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                        IsDisabled = c.Boolean(nullable: false),
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
                        ArabicName = c.String(maxLength: 150),
                        UnitCode = c.String(maxLength: 150, unicode: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "PosCloud.TransMasterPaymentMethods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Method = c.String(maxLength: 150, unicode: false),
                        Amount = c.Double(nullable: false),
                        TransMasterId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .ForeignKey("PosCloud.TransMaster", t => new { t.TransMasterId, t.StoreId })
                .Index(t => t.StoreId)
                .Index(t => new { t.TransMasterId, t.StoreId });
            
            CreateTable(
                "PosCloud.Warehouses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        ArabicName = c.String(maxLength: 150),
                        ClientId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                        IsDisabled = c.Boolean(nullable: false),
                        Store_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("PosCloud.Clients", t => t.ClientId)
                .ForeignKey("PosCloud.Stores", t => t.Store_Id)
                .Index(t => t.ClientId)
                .Index(t => t.Store_Id);
            
            CreateTable(
                "PosCloud.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        ArabicName = c.String(maxLength: 150),
                        Address = c.String(nullable: false, maxLength: 150, unicode: false),
                        Password = c.String(),
                        Contact = c.String(nullable: false, maxLength: 150, unicode: false),
                        Email = c.String(),
                        VatNumber = c.String(maxLength: 150, unicode: false),
                        City = c.String(maxLength: 150, unicode: false),
                        State = c.String(maxLength: 150, unicode: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "PosCloud.ReportLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        Details = c.String(maxLength: 4000),
                        Path = c.String(nullable: false, maxLength: 4000),
                        Status = c.String(maxLength: 150, unicode: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "PosCloud.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        ArabicName = c.String(maxLength: 150),
                        StateId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                        IsDisabled = c.Boolean(nullable: false),
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
                        ArabicName = c.String(maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "PosCloud.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        ArabicName = c.String(),
                        Address = c.String(maxLength: 150),
                        Contact = c.String(maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "PosCloud.ProductCategoryGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ArabicName = c.String(),
                        StoreId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "PosCloud.Sizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        ArabicName = c.String(maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("PosCloud.Sizes", "StoreId", "PosCloud.Stores");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("PosCloud.ProductCategoryGroups", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Cities", "StateId", "PosCloud.States");
            DropForeignKey("PosCloud.AppCounters", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.AppCounters", new[] { "DeviceId", "StoreId" }, "PosCloud.Devices");
            DropForeignKey("PosCloud.Devices", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.IncrementalSyncronizations", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Warehouses", "Store_Id", "PosCloud.Stores");
            DropForeignKey("PosCloud.TransDetails", "Store_Id", "PosCloud.Stores");
            DropForeignKey("PosCloud.ReportLogs", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ModifierLinkProducts", "Store_Id1", "PosCloud.Stores");
            DropForeignKey("PosCloud.ModifierLinkProducts", "Store_Id", "PosCloud.Stores");
            DropForeignKey("PosCloud.Modifier", "Store_Id", "PosCloud.Stores");
            DropForeignKey("PosCloud.Discounts", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Stores", "ClientId", "PosCloud.Clients");
            DropForeignKey("PosCloud.TransMaster", "WarehouseId", "PosCloud.Warehouses");
            DropForeignKey("PosCloud.Warehouses", "ClientId", "PosCloud.Clients");
            DropForeignKey("PosCloud.TransMasterPaymentMethods", new[] { "TransMasterId", "StoreId" }, "PosCloud.TransMaster");
            DropForeignKey("PosCloud.TransMasterPaymentMethods", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TransMaster", new[] { "DiscountId", "StoreId" }, "PosCloud.TimedEvents");
            DropForeignKey("PosCloud.TimedEventProducts", new[] { "TimedEventId", "StoreId" }, "PosCloud.TimedEvents");
            DropForeignKey("PosCloud.TimedEventProducts", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TimedEventProducts", new[] { "ProductCode", "StoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.Products", new[] { "TaxId", "StoreId" }, "PosCloud.Taxes");
            DropForeignKey("PosCloud.Products", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Products", new[] { "SectionId", "StoreId" }, "PosCloud.Sections");
            DropForeignKey("PosCloud.Products", new[] { "UnitId", "StoreId" }, "PosCloud.Units");
            DropForeignKey("PosCloud.Units", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Products", new[] { "CategoryId", "StoreId" }, "PosCloud.ProductCategories");
            DropForeignKey("PosCloud.ProductCategories", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Recipes", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Recipes", new[] { "ProductCode", "StoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.Recipes", new[] { "IngredientCode", "StoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.ProductsSubs", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ProductsSubs", new[] { "ProductCode", "StoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.TransDetails", new[] { "TransMasterId", "StoreId" }, "PosCloud.TransMaster");
            DropForeignKey("PosCloud.TransDetails", new[] { "DiscountId", "StoreId" }, "PosCloud.TimedEvents");
            DropForeignKey("PosCloud.TransDetails", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TransDetails", new[] { "ProductCode", "StoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.ModifierTransDetails", new[] { "TransDetailId", "StoreId" }, "PosCloud.TransDetails");
            DropForeignKey("PosCloud.ModifierTransDetails", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ModifierTransDetails", new[] { "ModifierOptionId", "StoreId" }, "PosCloud.ModifierOptions");
            DropForeignKey("PosCloud.ModifierOptions", new[] { "TaxId", "StoreId" }, "PosCloud.Taxes");
            DropForeignKey("PosCloud.Taxes", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ModifierOptions", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ModifierOptions", new[] { "ModifierId", "StoreId" }, "PosCloud.Modifier");
            DropForeignKey("PosCloud.Modifier", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ModifierLinkProducts", "ProductStoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ModifierLinkProducts", new[] { "ProductCode", "ProductStoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.ModifierLinkProducts", "ModifierStoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ModifierLinkProducts", new[] { "ModifierId", "ModifierStoreId" }, "PosCloud.Modifier");
            DropForeignKey("PosCloud.TransDetails", new[] { "EmployeeId", "StoreId" }, "PosCloud.Employees");
            DropForeignKey("PosCloud.Employees", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Employees", new[] { "ShiftId", "StoreId" }, "PosCloud.Shifts");
            DropForeignKey("PosCloud.Expenses", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Expenses", new[] { "ExpenseHeadId", "StoreId" }, "PosCloud.ExpenseHeads");
            DropForeignKey("PosCloud.ExpenseHeads", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Expenses", new[] { "EmployeeId", "StoreId" }, "PosCloud.Employees");
            DropForeignKey("PosCloud.Employees", new[] { "DesignationId", "StoreId" }, "PosCloud.Designations");
            DropForeignKey("PosCloud.Designations", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Employees", new[] { "DepartmentId", "StoreId" }, "PosCloud.Departments");
            DropForeignKey("PosCloud.Departments", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.UserStores", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.UserStores", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("PosCloud.TillOperations", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TillOperations", new[] { "ShiftId", "StoreId" }, "PosCloud.Shifts");
            DropForeignKey("PosCloud.Shifts", "StoreId", "PosCloud.Stores");
            DropForeignKey("dbo.AspNetUsers", new[] { "Shift_ShiftId", "Shift_StoreId" }, "PosCloud.Shifts");
            DropForeignKey("PosCloud.TillOperations", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "StoreId", "PosCloud.Stores");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", new[] { "POSTerminalId", "StoreId" }, "PosCloud.POSTerminals");
            DropForeignKey("PosCloud.POSTerminals", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.POSTerminals", new[] { "SectionId", "StoreId" }, "PosCloud.Sections");
            DropForeignKey("PosCloud.Sections", "StoreId", "PosCloud.Stores");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", new[] { "EmployeeId", "StoreId" }, "PosCloud.Employees");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ComboProductsTransDetails", new[] { "Id", "StoreId" }, "PosCloud.TransDetails");
            DropForeignKey("dbo.ComboProductsTransDetails", "StoreId", "PosCloud.Stores");
            DropForeignKey("dbo.ComboProductsTransDetails", new[] { "ProductsSub_ComboProductCode", "ProductsSub_StoreId", "ProductsSub_ProductCode" }, "PosCloud.ProductsSubs");
            DropForeignKey("PosCloud.ProductsSubs", new[] { "ComboProductCode", "StoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.TimedEvents", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TransMaster", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TransMaster", new[] { "DineTableId", "StoreId" }, "PosCloud.DineTables");
            DropForeignKey("PosCloud.DineTables", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.DineTables", new[] { "FloorId", "StoreId" }, "PosCloud.Floors");
            DropForeignKey("PosCloud.Floors", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TransMaster", new[] { "DeviceId", "StoreId" }, "PosCloud.Devices");
            DropForeignKey("PosCloud.TransMaster", new[] { "BusinessPartnerId", "StoreId" }, "PosCloud.BusinessPartners");
            DropForeignKey("PosCloud.BusinessPartners", "StoreId", "PosCloud.Stores");
            DropForeignKey("dbo.AspNetRoles", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.SecurityRights", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.SecurityRights", "SecurityObjectId", "PosCloud.SecurityObjects");
            DropForeignKey("PosCloud.SecurityRights", "IdentityUserRoleId", "dbo.AspNetRoles");
            DropForeignKey("PosCloud.IncrementalSyncronizations", new[] { "DeviceId", "StoreId" }, "PosCloud.Devices");
            DropIndex("PosCloud.Sizes", new[] { "StoreId" });
            DropIndex("PosCloud.ProductCategoryGroups", new[] { "StoreId" });
            DropIndex("PosCloud.Cities", new[] { "StateId" });
            DropIndex("PosCloud.ReportLogs", new[] { "StoreId" });
            DropIndex("PosCloud.Discounts", new[] { "StoreId" });
            DropIndex("PosCloud.Warehouses", new[] { "Store_Id" });
            DropIndex("PosCloud.Warehouses", new[] { "ClientId" });
            DropIndex("PosCloud.TransMasterPaymentMethods", new[] { "TransMasterId", "StoreId" });
            DropIndex("PosCloud.TransMasterPaymentMethods", new[] { "StoreId" });
            DropIndex("PosCloud.Units", new[] { "StoreId" });
            DropIndex("PosCloud.ProductCategories", new[] { "StoreId" });
            DropIndex("PosCloud.Recipes", new[] { "ProductCode", "StoreId" });
            DropIndex("PosCloud.Recipes", new[] { "IngredientCode", "StoreId" });
            DropIndex("PosCloud.Taxes", new[] { "StoreId" });
            DropIndex("PosCloud.ModifierLinkProducts", new[] { "Store_Id1" });
            DropIndex("PosCloud.ModifierLinkProducts", new[] { "Store_Id" });
            DropIndex("PosCloud.ModifierLinkProducts", new[] { "ProductCode", "ProductStoreId" });
            DropIndex("PosCloud.ModifierLinkProducts", new[] { "ModifierId", "ModifierStoreId" });
            DropIndex("PosCloud.Modifier", new[] { "Store_Id" });
            DropIndex("PosCloud.Modifier", new[] { "StoreId" });
            DropIndex("PosCloud.ModifierOptions", new[] { "TaxId", "StoreId" });
            DropIndex("PosCloud.ModifierOptions", new[] { "ModifierId", "StoreId" });
            DropIndex("PosCloud.ModifierTransDetails", new[] { "TransDetailId", "StoreId" });
            DropIndex("PosCloud.ModifierTransDetails", new[] { "ModifierOptionId", "StoreId" });
            DropIndex("PosCloud.ExpenseHeads", new[] { "StoreId" });
            DropIndex("PosCloud.Expenses", new[] { "ExpenseHeadId", "StoreId" });
            DropIndex("PosCloud.Expenses", new[] { "EmployeeId", "StoreId" });
            DropIndex("PosCloud.Designations", new[] { "StoreId" });
            DropIndex("PosCloud.Departments", new[] { "StoreId" });
            DropIndex("PosCloud.UserStores", new[] { "StoreId" });
            DropIndex("PosCloud.UserStores", new[] { "ApplicationUserId" });
            DropIndex("PosCloud.Shifts", new[] { "StoreId" });
            DropIndex("PosCloud.TillOperations", new[] { "ApplicationUserId" });
            DropIndex("PosCloud.TillOperations", new[] { "ShiftId", "StoreId" });
            DropIndex("PosCloud.Sections", new[] { "StoreId" });
            DropIndex("PosCloud.POSTerminals", new[] { "SectionId", "StoreId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Shift_ShiftId", "Shift_StoreId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "POSTerminalId", "StoreId" });
            DropIndex("dbo.AspNetUsers", new[] { "EmployeeId", "StoreId" });
            DropIndex("PosCloud.Employees", new[] { "ShiftId", "StoreId" });
            DropIndex("PosCloud.Employees", new[] { "DesignationId", "StoreId" });
            DropIndex("PosCloud.Employees", new[] { "DepartmentId", "StoreId" });
            DropIndex("PosCloud.TransDetails", new[] { "Store_Id" });
            DropIndex("PosCloud.TransDetails", new[] { "TransMasterId", "StoreId" });
            DropIndex("PosCloud.TransDetails", new[] { "DiscountId", "StoreId" });
            DropIndex("PosCloud.TransDetails", new[] { "ProductCode", "StoreId" });
            DropIndex("PosCloud.TransDetails", new[] { "EmployeeId", "StoreId" });
            DropIndex("dbo.ComboProductsTransDetails", new[] { "ProductsSub_ComboProductCode", "ProductsSub_StoreId", "ProductsSub_ProductCode" });
            DropIndex("dbo.ComboProductsTransDetails", new[] { "StoreId" });
            DropIndex("dbo.ComboProductsTransDetails", new[] { "Id", "StoreId" });
            DropIndex("PosCloud.ProductsSubs", new[] { "ProductCode", "StoreId" });
            DropIndex("PosCloud.ProductsSubs", new[] { "ComboProductCode", "StoreId" });
            DropIndex("PosCloud.Products", new[] { "TaxId", "StoreId" });
            DropIndex("PosCloud.Products", new[] { "SectionId", "StoreId" });
            DropIndex("PosCloud.Products", new[] { "UnitId", "StoreId" });
            DropIndex("PosCloud.Products", new[] { "CategoryId", "StoreId" });
            DropIndex("PosCloud.TimedEventProducts", new[] { "TimedEventId", "StoreId" });
            DropIndex("PosCloud.TimedEventProducts", new[] { "ProductCode", "StoreId" });
            DropIndex("PosCloud.TimedEvents", new[] { "StoreId" });
            DropIndex("PosCloud.Floors", new[] { "StoreId" });
            DropIndex("PosCloud.DineTables", new[] { "FloorId", "StoreId" });
            DropIndex("PosCloud.TransMaster", new[] { "WarehouseId" });
            DropIndex("PosCloud.TransMaster", new[] { "TransCode" });
            DropIndex("PosCloud.TransMaster", new[] { "DiscountId", "StoreId" });
            DropIndex("PosCloud.TransMaster", new[] { "DineTableId", "StoreId" });
            DropIndex("PosCloud.TransMaster", new[] { "DeviceId", "StoreId" });
            DropIndex("PosCloud.TransMaster", new[] { "BusinessPartnerId", "StoreId" });
            DropIndex("PosCloud.BusinessPartners", new[] { "StoreId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("PosCloud.SecurityRights", new[] { "SecurityObjectId" });
            DropIndex("PosCloud.SecurityRights", new[] { "StoreId" });
            DropIndex("PosCloud.SecurityRights", new[] { "IdentityUserRoleId" });
            DropIndex("dbo.AspNetRoles", new[] { "StoreId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("PosCloud.Stores", new[] { "ClientId" });
            DropIndex("PosCloud.IncrementalSyncronizations", new[] { "DeviceId", "StoreId" });
            DropIndex("PosCloud.Devices", new[] { "StoreId" });
            DropIndex("PosCloud.AppCounters", new[] { "DeviceId", "StoreId" });
            DropTable("PosCloud.Sizes");
            DropTable("PosCloud.ProductCategoryGroups");
            DropTable("PosCloud.Locations");
            DropTable("PosCloud.States");
            DropTable("PosCloud.Cities");
            DropTable("PosCloud.ReportLogs");
            DropTable("PosCloud.Discounts");
            DropTable("PosCloud.Clients");
            DropTable("PosCloud.Warehouses");
            DropTable("PosCloud.TransMasterPaymentMethods");
            DropTable("PosCloud.Units");
            DropTable("PosCloud.ProductCategories");
            DropTable("PosCloud.Recipes");
            DropTable("PosCloud.Taxes");
            DropTable("PosCloud.ModifierLinkProducts");
            DropTable("PosCloud.Modifier");
            DropTable("PosCloud.ModifierOptions");
            DropTable("PosCloud.ModifierTransDetails");
            DropTable("PosCloud.ExpenseHeads");
            DropTable("PosCloud.Expenses");
            DropTable("PosCloud.Designations");
            DropTable("PosCloud.Departments");
            DropTable("PosCloud.UserStores");
            DropTable("PosCloud.Shifts");
            DropTable("PosCloud.TillOperations");
            DropTable("PosCloud.Sections");
            DropTable("PosCloud.POSTerminals");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("PosCloud.Employees");
            DropTable("PosCloud.TransDetails");
            DropTable("dbo.ComboProductsTransDetails");
            DropTable("PosCloud.ProductsSubs");
            DropTable("PosCloud.Products");
            DropTable("PosCloud.TimedEventProducts");
            DropTable("PosCloud.TimedEvents");
            DropTable("PosCloud.Floors");
            DropTable("PosCloud.DineTables");
            DropTable("PosCloud.TransMaster");
            DropTable("PosCloud.BusinessPartners");
            DropTable("dbo.AspNetUserRoles");
            DropTable("PosCloud.SecurityObjects");
            DropTable("PosCloud.SecurityRights");
            DropTable("dbo.AspNetRoles");
            DropTable("PosCloud.Stores");
            DropTable("PosCloud.IncrementalSyncronizations");
            DropTable("PosCloud.Devices");
            DropTable("PosCloud.AppCounters");
        }
    }
}
