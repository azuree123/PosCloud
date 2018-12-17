namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifierLinkProducts : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("PosCloud.ModifierLinkProducts", "Store_Id1", "PosCloud.Stores");
            DropForeignKey("PosCloud.ModifierLinkProducts", "Store_Id", "PosCloud.Stores");
            DropForeignKey("PosCloud.ModifierLinkProducts", "ProductStoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ModifierLinkProducts", new[] { "ProductCode", "ProductStoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.ModifierLinkProducts", "ModifierStoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ModifierLinkProducts", new[] { "ModifierId", "ModifierStoreId" }, "PosCloud.Modifier");
            DropIndex("PosCloud.ModifierLinkProducts", new[] { "Store_Id1" });
            DropIndex("PosCloud.ModifierLinkProducts", new[] { "Store_Id" });
            DropIndex("PosCloud.ModifierLinkProducts", new[] { "ProductCode", "ProductStoreId" });
            DropIndex("PosCloud.ModifierLinkProducts", new[] { "ModifierId", "ModifierStoreId" });
            DropTable("PosCloud.ModifierLinkProducts");
        }
    }
}
