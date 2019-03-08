namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
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
                        IsDisabled = c.Boolean(nullable: false),
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
                        IsDisabled = c.Boolean(nullable: false),
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
                        IsDisabled = c.Boolean(nullable: false),
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
                "PosCloud.TransMaster",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        SessionCode = c.Int(nullable: false),
                        Type = c.String(maxLength: 3, fixedLength: true, unicode: false),
                        TransCode = c.String(maxLength: 25, unicode: false),
                        DeliveryType = c.String(maxLength: 25, unicode: false),
                        Address = c.String(maxLength: 150, unicode: false),
                        ContactNumber = c.String(maxLength: 25, unicode: false),
                        BusinessPartnerId = c.Int(nullable: false),
                        TransDate = c.DateTime(nullable: false),
                        Tax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransRef = c.String(maxLength: 25, unicode: false),
                        TransStatus = c.String(maxLength: 25, unicode: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Posted = c.Boolean(nullable: false),
                        ACRef = c.String(maxLength: 25, unicode: false),
                        PaymentMethod = c.String(),
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
                .ForeignKey("PosCloud.DineTables", t => new { t.DineTableId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .ForeignKey("PosCloud.TimedEvents", t => new { t.DiscountId, t.StoreId }, cascadeDelete: true)
                .Index(t => new { t.BusinessPartnerId, t.StoreId })
                .Index(t => new { t.DineTableId, t.StoreId })
                .Index(t => new { t.DiscountId, t.StoreId })
                .Index(t => t.TransCode, unique: true);
            
            CreateTable(
                "PosCloud.TimedEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
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
                "PosCloud.Recipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IngredientCode = c.String(nullable: false, maxLength: 150, unicode: false),
                        StoreId = c.Int(nullable: false),
                        ProductCode = c.String(nullable: false, maxLength: 150, unicode: false),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitId = c.Int(nullable: false),
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
                .ForeignKey("PosCloud.Units", t => new { t.UnitId, t.StoreId })
                .Index(t => new { t.IngredientCode, t.StoreId })
                .Index(t => new { t.ProductCode, t.StoreId })
                .Index(t => new { t.UnitId, t.StoreId });
            
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
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
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
                "PosCloud.Modifier",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
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
                "PosCloud.ModifierTransDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
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
                "PosCloud.TransDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Tax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransMasterId = c.Int(nullable: false),
                        ProductCode = c.String(nullable: false, maxLength: 150, unicode: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountId = c.Int(),
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
                .ForeignKey("PosCloud.Products", t => new { t.ProductCode, t.StoreId }, cascadeDelete: true)
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .ForeignKey("PosCloud.TimedEvents", t => new { t.DiscountId, t.StoreId }, cascadeDelete: true)
                .ForeignKey("PosCloud.TransMaster", t => new { t.TransMasterId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.Store_Id)
                .Index(t => new { t.ProductCode, t.StoreId })
                .Index(t => new { t.DiscountId, t.StoreId })
                .Index(t => new { t.TransMasterId, t.StoreId })
                .Index(t => t.Store_Id);
            
            CreateTable(
                "PosCloud.Taxes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
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
                        IsDisabled = c.Boolean(nullable: false),
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
                        IsDisabled = c.Boolean(nullable: false),
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
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PasswordEncrypt = c.String(),
                        EmployeeId = c.Int(),
                        StoreId = c.Int(),
                        POSTerminalId = c.Int(),
                        ShiftId = c.Int(),
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
                        Employee_Id = c.Int(),
                        Employee_StoreId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("PosCloud.Employees", t => new { t.Employee_Id, t.Employee_StoreId })
                .ForeignKey("PosCloud.POSTerminals", t => new { t.POSTerminalId, t.StoreId })
                .ForeignKey("PosCloud.Shifts", t => new { t.ShiftId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => new { t.POSTerminalId, t.StoreId })
                .Index(t => new { t.ShiftId, t.StoreId })
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
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => new { t.DepartmentId, t.StoreId })
                .Index(t => new { t.DesignationId, t.StoreId });
            
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
                "PosCloud.Shifts",
                c => new
                    {
                        ShiftId = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 10, unicode: false),
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
                        IsDisabled = c.Boolean(nullable: false),
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
                        IsDisabled = c.Boolean(nullable: false),
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
                        IsDisabled = c.Boolean(nullable: false),
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
                        IsDisabled = c.Boolean(nullable: false),
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
            DropForeignKey("PosCloud.Sizes", "StoreId", "PosCloud.Stores");
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
            DropForeignKey("PosCloud.ModifierLinkProducts", "Store_Id1", "PosCloud.Stores");
            DropForeignKey("PosCloud.ModifierLinkProducts", "Store_Id", "PosCloud.Stores");
            DropForeignKey("PosCloud.Modifier", "Store_Id", "PosCloud.Stores");
            DropForeignKey("PosCloud.Discounts", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TransMasterPaymentMethods", new[] { "TransMasterId", "StoreId" }, "PosCloud.TransMaster");
            DropForeignKey("PosCloud.TransMasterPaymentMethods", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TransMaster", new[] { "DiscountId", "StoreId" }, "PosCloud.TimedEvents");
            DropForeignKey("PosCloud.TimedEventProducts", new[] { "TimedEventId", "StoreId" }, "PosCloud.TimedEvents");
            DropForeignKey("PosCloud.TimedEventProducts", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TimedEventProducts", new[] { "ProductCode", "StoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.Products", new[] { "TaxId", "StoreId" }, "PosCloud.Taxes");
            DropForeignKey("PosCloud.Products", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Products", new[] { "SectionId", "StoreId" }, "PosCloud.Sections");
            DropForeignKey("PosCloud.Sections", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.POSTerminals", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.POSTerminals", new[] { "SectionId", "StoreId" }, "PosCloud.Sections");
            DropForeignKey("dbo.AspNetUsers", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TillOperations", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TillOperations", new[] { "ShiftId", "StoreId" }, "PosCloud.Shifts");
            DropForeignKey("PosCloud.TillOperations", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("PosCloud.Shifts", "StoreId", "PosCloud.Stores");
            DropForeignKey("dbo.AspNetUsers", new[] { "ShiftId", "StoreId" }, "PosCloud.Shifts");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", new[] { "POSTerminalId", "StoreId" }, "PosCloud.POSTerminals");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", new[] { "Employee_Id", "Employee_StoreId" }, "PosCloud.Employees");
            DropForeignKey("PosCloud.Employees", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Expenses", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Expenses", new[] { "ExpenseHeadId", "StoreId" }, "PosCloud.ExpenseHeads");
            DropForeignKey("PosCloud.ExpenseHeads", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Expenses", new[] { "EmployeeId", "StoreId" }, "PosCloud.Employees");
            DropForeignKey("PosCloud.Employees", new[] { "DesignationId", "StoreId" }, "PosCloud.Designations");
            DropForeignKey("PosCloud.Designations", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Employees", new[] { "DepartmentId", "StoreId" }, "PosCloud.Departments");
            DropForeignKey("PosCloud.Departments", "StoreId", "PosCloud.Stores");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("PosCloud.Products", new[] { "UnitId", "StoreId" }, "PosCloud.Units");
            DropForeignKey("PosCloud.Products", new[] { "CategoryId", "StoreId" }, "PosCloud.ProductCategories");
            DropForeignKey("PosCloud.ProductCategories", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ModifierLinkProducts", "ProductStoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ModifierLinkProducts", new[] { "ProductCode", "ProductStoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.ModifierLinkProducts", "ModifierStoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ModifierLinkProducts", new[] { "ModifierId", "ModifierStoreId" }, "PosCloud.Modifier");
            DropForeignKey("PosCloud.Modifier", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ProductModifiers", new[] { "ModifierId", "ModifierStoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.ProductModifiers", new[] { "ProductCode", "ProductStoreCode" }, "PosCloud.Modifier");
            DropForeignKey("PosCloud.ModifierOptions", new[] { "TaxId", "StoreId" }, "PosCloud.Taxes");
            DropForeignKey("PosCloud.Taxes", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ModifierOptions", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ModifierTransDetails", new[] { "TransDetailId", "StoreId" }, "PosCloud.TransDetails");
            DropForeignKey("PosCloud.TransDetails", new[] { "TransMasterId", "StoreId" }, "PosCloud.TransMaster");
            DropForeignKey("PosCloud.TransDetails", new[] { "DiscountId", "StoreId" }, "PosCloud.TimedEvents");
            DropForeignKey("PosCloud.TransDetails", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TransDetails", new[] { "ProductCode", "StoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.ModifierTransDetails", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ModifierTransDetails", new[] { "ModifierOptionId", "StoreId" }, "PosCloud.ModifierOptions");
            DropForeignKey("PosCloud.ModifierOptions", new[] { "ModifierId", "StoreId" }, "PosCloud.Modifier");
            DropForeignKey("PosCloud.Recipes", new[] { "UnitId", "StoreId" }, "PosCloud.Units");
            DropForeignKey("PosCloud.Units", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Recipes", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Recipes", new[] { "ProductCode", "StoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.Recipes", new[] { "IngredientCode", "StoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.ProductsSubs", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ProductsSubs", new[] { "ProductCode", "StoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.ProductsSubs", new[] { "ComboProductCode", "StoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.TimedEvents", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TransMaster", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TransMaster", new[] { "DineTableId", "StoreId" }, "PosCloud.DineTables");
            DropForeignKey("PosCloud.TransMaster", new[] { "BusinessPartnerId", "StoreId" }, "PosCloud.BusinessPartners");
            DropForeignKey("PosCloud.DineTables", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.DineTables", new[] { "FloorId", "StoreId" }, "PosCloud.Floors");
            DropForeignKey("PosCloud.Floors", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Devices", "StoreId", "PosCloud.Stores");
            DropIndex("PosCloud.ProductModifiers", new[] { "ModifierId", "ModifierStoreId" });
            DropIndex("PosCloud.ProductModifiers", new[] { "ProductCode", "ProductStoreCode" });
            DropIndex("PosCloud.Sizes", new[] { "StoreId" });
            DropIndex("PosCloud.ProductCategoryGroups", new[] { "StoreId" });
            DropIndex("PosCloud.Cities", new[] { "StateId" });
            DropIndex("dbo.AspNetRoles", new[] { "StoreId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("PosCloud.SecurityRights", new[] { "SecurityObjectId" });
            DropIndex("PosCloud.SecurityRights", new[] { "StoreId" });
            DropIndex("PosCloud.SecurityRights", new[] { "IdentityUserRoleId" });
            DropIndex("PosCloud.ReportLogs", new[] { "StoreId" });
            DropIndex("PosCloud.Discounts", new[] { "StoreId" });
            DropIndex("PosCloud.TransMasterPaymentMethods", new[] { "TransMasterId", "StoreId" });
            DropIndex("PosCloud.TransMasterPaymentMethods", new[] { "StoreId" });
            DropIndex("PosCloud.TillOperations", new[] { "ApplicationUserId" });
            DropIndex("PosCloud.TillOperations", new[] { "ShiftId", "StoreId" });
            DropIndex("PosCloud.Shifts", new[] { "StoreId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("PosCloud.ExpenseHeads", new[] { "StoreId" });
            DropIndex("PosCloud.Expenses", new[] { "ExpenseHeadId", "StoreId" });
            DropIndex("PosCloud.Expenses", new[] { "EmployeeId", "StoreId" });
            DropIndex("PosCloud.Designations", new[] { "StoreId" });
            DropIndex("PosCloud.Departments", new[] { "StoreId" });
            DropIndex("PosCloud.Employees", new[] { "DesignationId", "StoreId" });
            DropIndex("PosCloud.Employees", new[] { "DepartmentId", "StoreId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Employee_Id", "Employee_StoreId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "ShiftId", "StoreId" });
            DropIndex("dbo.AspNetUsers", new[] { "POSTerminalId", "StoreId" });
            DropIndex("PosCloud.POSTerminals", new[] { "SectionId", "StoreId" });
            DropIndex("PosCloud.Sections", new[] { "StoreId" });
            DropIndex("PosCloud.ProductCategories", new[] { "StoreId" });
            DropIndex("PosCloud.Taxes", new[] { "StoreId" });
            DropIndex("PosCloud.TransDetails", new[] { "Store_Id" });
            DropIndex("PosCloud.TransDetails", new[] { "TransMasterId", "StoreId" });
            DropIndex("PosCloud.TransDetails", new[] { "DiscountId", "StoreId" });
            DropIndex("PosCloud.TransDetails", new[] { "ProductCode", "StoreId" });
            DropIndex("PosCloud.ModifierTransDetails", new[] { "TransDetailId", "StoreId" });
            DropIndex("PosCloud.ModifierTransDetails", new[] { "ModifierOptionId", "StoreId" });
            DropIndex("PosCloud.ModifierOptions", new[] { "TaxId", "StoreId" });
            DropIndex("PosCloud.ModifierOptions", new[] { "ModifierId", "StoreId" });
            DropIndex("PosCloud.Modifier", new[] { "Store_Id" });
            DropIndex("PosCloud.Modifier", new[] { "StoreId" });
            DropIndex("PosCloud.ModifierLinkProducts", new[] { "Store_Id1" });
            DropIndex("PosCloud.ModifierLinkProducts", new[] { "Store_Id" });
            DropIndex("PosCloud.ModifierLinkProducts", new[] { "ProductCode", "ProductStoreId" });
            DropIndex("PosCloud.ModifierLinkProducts", new[] { "ModifierId", "ModifierStoreId" });
            DropIndex("PosCloud.Units", new[] { "StoreId" });
            DropIndex("PosCloud.Recipes", new[] { "UnitId", "StoreId" });
            DropIndex("PosCloud.Recipes", new[] { "ProductCode", "StoreId" });
            DropIndex("PosCloud.Recipes", new[] { "IngredientCode", "StoreId" });
            DropIndex("PosCloud.ProductsSubs", new[] { "ProductCode", "StoreId" });
            DropIndex("PosCloud.ProductsSubs", new[] { "ComboProductCode", "StoreId" });
            DropIndex("PosCloud.Products", new[] { "TaxId", "StoreId" });
            DropIndex("PosCloud.Products", new[] { "SectionId", "StoreId" });
            DropIndex("PosCloud.Products", new[] { "UnitId", "StoreId" });
            DropIndex("PosCloud.Products", new[] { "CategoryId", "StoreId" });
            DropIndex("PosCloud.TimedEventProducts", new[] { "TimedEventId", "StoreId" });
            DropIndex("PosCloud.TimedEventProducts", new[] { "ProductCode", "StoreId" });
            DropIndex("PosCloud.TimedEvents", new[] { "StoreId" });
            DropIndex("PosCloud.TransMaster", new[] { "TransCode" });
            DropIndex("PosCloud.TransMaster", new[] { "DiscountId", "StoreId" });
            DropIndex("PosCloud.TransMaster", new[] { "DineTableId", "StoreId" });
            DropIndex("PosCloud.TransMaster", new[] { "BusinessPartnerId", "StoreId" });
            DropIndex("PosCloud.Floors", new[] { "StoreId" });
            DropIndex("PosCloud.DineTables", new[] { "FloorId", "StoreId" });
            DropIndex("PosCloud.Devices", new[] { "StoreId" });
            DropIndex("PosCloud.BusinessPartners", new[] { "StoreId" });
            DropTable("PosCloud.ProductModifiers");
            DropTable("PosCloud.Sizes");
            DropTable("PosCloud.ProductCategoryGroups");
            DropTable("PosCloud.Locations");
            DropTable("PosCloud.Clients");
            DropTable("PosCloud.States");
            DropTable("PosCloud.Cities");
            DropTable("PosCloud.SecurityObjects");
            DropTable("dbo.AspNetRoles");
            DropTable("PosCloud.SecurityRights");
            DropTable("PosCloud.ReportLogs");
            DropTable("PosCloud.Discounts");
            DropTable("PosCloud.TransMasterPaymentMethods");
            DropTable("PosCloud.TillOperations");
            DropTable("PosCloud.Shifts");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("PosCloud.ExpenseHeads");
            DropTable("PosCloud.Expenses");
            DropTable("PosCloud.Designations");
            DropTable("PosCloud.Departments");
            DropTable("PosCloud.Employees");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("PosCloud.POSTerminals");
            DropTable("PosCloud.Sections");
            DropTable("PosCloud.ProductCategories");
            DropTable("PosCloud.Taxes");
            DropTable("PosCloud.TransDetails");
            DropTable("PosCloud.ModifierTransDetails");
            DropTable("PosCloud.ModifierOptions");
            DropTable("PosCloud.Modifier");
            DropTable("PosCloud.ModifierLinkProducts");
            DropTable("PosCloud.Units");
            DropTable("PosCloud.Recipes");
            DropTable("PosCloud.ProductsSubs");
            DropTable("PosCloud.Products");
            DropTable("PosCloud.TimedEventProducts");
            DropTable("PosCloud.TimedEvents");
            DropTable("PosCloud.TransMaster");
            DropTable("PosCloud.Floors");
            DropTable("PosCloud.DineTables");
            DropTable("PosCloud.Devices");
            DropTable("PosCloud.Stores");
            DropTable("PosCloud.BusinessPartners");
            DropTable("dbo.AppCounters");
        }
    }
}