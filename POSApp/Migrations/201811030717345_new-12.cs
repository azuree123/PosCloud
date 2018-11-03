namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new12 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ReportsLogs");
            RenameTable(name: "dbo.ReportsLogs", newName: "ReportLogs");
            MoveTable(name: "dbo.ReportLogs", newSchema: "PosCloud");
            MoveTable(name: "dbo.Clients", newSchema: "PosCloud");
            MoveTable(name: "dbo.ProductCategoryGroups", newSchema: "PosCloud");
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
                "PosCloud.ProductModifiers",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        ProductStoreId = c.Int(nullable: false),
                        ModifierId = c.Int(nullable: false),
                        ModifierStoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.ProductStoreId, t.ModifierId, t.ModifierStoreId })
                .ForeignKey("PosCloud.Modifier", t => new { t.ProductId, t.ProductStoreId }, cascadeDelete: true)
                .ForeignKey("PosCloud.Products", t => new { t.ModifierId, t.ModifierStoreId }, cascadeDelete: true)
                .Index(t => new { t.ProductId, t.ProductStoreId })
                .Index(t => new { t.ModifierId, t.ModifierStoreId });
            
            CreateTable(
                "PosCloud.TimedEventProducts",
                c => new
                    {
                        TimedEventId = c.Int(nullable: false),
                        TimedEventStoreId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ProductStoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TimedEventId, t.TimedEventStoreId, t.ProductId, t.ProductStoreId })
                .ForeignKey("PosCloud.Products", t => new { t.TimedEventId, t.TimedEventStoreId }, cascadeDelete: true)
                .ForeignKey("PosCloud.TimedEvents", t => new { t.ProductId, t.ProductStoreId }, cascadeDelete: true)
                .Index(t => new { t.TimedEventId, t.TimedEventStoreId })
                .Index(t => new { t.ProductId, t.ProductStoreId });
            
            AlterColumn("PosCloud.ReportLogs", "Name", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("PosCloud.ReportLogs", "Details", c => c.String(maxLength: 8000, unicode: false));
            AlterColumn("PosCloud.ReportLogs", "Path", c => c.String(nullable: false, maxLength: 8000, unicode: false));
            AlterColumn("PosCloud.ReportLogs", "Status", c => c.String(maxLength: 150, unicode: false));
            AlterColumn("PosCloud.ReportLogs", "Code", c => c.String(maxLength: 150, unicode: false));
            AddPrimaryKey("PosCloud.ReportLogs", new[] { "Id", "StoreId" });
        }
        
        public override void Down()
        {
            DropForeignKey("PosCloud.Modifier", "Store_Id", "PosCloud.Stores");
            DropForeignKey("PosCloud.TimedEventProducts", new[] { "ProductId", "ProductStoreId" }, "PosCloud.TimedEvents");
            DropForeignKey("PosCloud.TimedEventProducts", new[] { "TimedEventId", "TimedEventStoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.TimedEvents", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.Modifier", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ProductModifiers", new[] { "ModifierId", "ModifierStoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.ProductModifiers", new[] { "ProductId", "ProductStoreId" }, "PosCloud.Modifier");
            DropForeignKey("PosCloud.ModifierOptions", new[] { "TaxId", "StoreId" }, "PosCloud.Taxes");
            DropForeignKey("PosCloud.ModifierOptions", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ModifierOptions", new[] { "ModifierId", "StoreId" }, "PosCloud.Modifier");
            DropIndex("PosCloud.TimedEventProducts", new[] { "ProductId", "ProductStoreId" });
            DropIndex("PosCloud.TimedEventProducts", new[] { "TimedEventId", "TimedEventStoreId" });
            DropIndex("PosCloud.ProductModifiers", new[] { "ModifierId", "ModifierStoreId" });
            DropIndex("PosCloud.ProductModifiers", new[] { "ProductId", "ProductStoreId" });
            DropIndex("PosCloud.TimedEvents", new[] { "StoreId" });
            DropIndex("PosCloud.ModifierOptions", new[] { "TaxId", "StoreId" });
            DropIndex("PosCloud.ModifierOptions", new[] { "ModifierId", "StoreId" });
            DropIndex("PosCloud.Modifier", new[] { "Store_Id" });
            DropIndex("PosCloud.Modifier", new[] { "StoreId" });
            DropPrimaryKey("PosCloud.ReportLogs");
            AlterColumn("PosCloud.ReportLogs", "Code", c => c.String());
            AlterColumn("PosCloud.ReportLogs", "Status", c => c.String());
            AlterColumn("PosCloud.ReportLogs", "Path", c => c.String());
            AlterColumn("PosCloud.ReportLogs", "Details", c => c.String());
            AlterColumn("PosCloud.ReportLogs", "Name", c => c.String());
            DropTable("PosCloud.TimedEventProducts");
            DropTable("PosCloud.ProductModifiers");
            DropTable("PosCloud.TimedEvents");
            DropTable("PosCloud.ModifierOptions");
            DropTable("PosCloud.Modifier");
            AddPrimaryKey("PosCloud.ReportLogs", "Id");
            MoveTable(name: "PosCloud.ProductCategoryGroups", newSchema: "dbo");
            MoveTable(name: "PosCloud.Clients", newSchema: "dbo");
            MoveTable(name: "PosCloud.ReportLogs", newSchema: "dbo");
            RenameTable(name: "dbo.ReportLogs", newName: "ReportsLogs");
        }
    }
}
