namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update8 : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductCategoryGroups", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.BusinessPartners", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TransDetails", "Store_Id", "PosCloud.Stores");
            DropForeignKey("PosCloud.TransDetails", new[] { "TransMasterId", "StoreId" }, "PosCloud.TransMaster");
            DropForeignKey("PosCloud.TransMaster", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TransMaster", new[] { "BusinessPartnerId", "StoreId" }, "PosCloud.BusinessPartners");
            DropForeignKey("PosCloud.TransDetails", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TransDetails", new[] { "ProductId", "StoreId" }, "PosCloud.Products");
            DropIndex("dbo.ProductCategoryGroups", new[] { "StoreId" });
            DropIndex("PosCloud.TransMaster", new[] { "TransCode" });
            DropIndex("PosCloud.TransMaster", new[] { "BusinessPartnerId", "StoreId" });
            DropIndex("PosCloud.TransDetails", new[] { "Store_Id" });
            DropIndex("PosCloud.TransDetails", new[] { "TransMasterId", "StoreId" });
            DropIndex("PosCloud.TransDetails", new[] { "ProductId", "StoreId" });
            DropIndex("PosCloud.BusinessPartners", new[] { "StoreId" });
            DropTable("dbo.ProductCategoryGroups");
            DropTable("PosCloud.TransMaster");
            DropTable("PosCloud.TransDetails");
            DropTable("PosCloud.BusinessPartners");
        }
    }
}
