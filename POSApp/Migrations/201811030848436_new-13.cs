namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new13 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "PosCloud.ProductsSubs",
                c => new
                    {
                        ComboProductId = c.Int(nullable: false),
                        StoreId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Qty = c.Double(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => new { t.ComboProductId, t.StoreId, t.ProductId })
                .ForeignKey("PosCloud.Products", t => new { t.ComboProductId, t.StoreId })
                .ForeignKey("PosCloud.Products", t => new { t.ProductId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => new { t.ComboProductId, t.StoreId })
                .Index(t => new { t.ProductId, t.StoreId });
            
        }
        
        public override void Down()
        {
            DropForeignKey("PosCloud.ProductsSubs", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ProductsSubs", new[] { "ProductId", "StoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.ProductsSubs", new[] { "ComboProductId", "StoreId" }, "PosCloud.Products");
            DropIndex("PosCloud.ProductsSubs", new[] { "ProductId", "StoreId" });
            DropIndex("PosCloud.ProductsSubs", new[] { "ComboProductId", "StoreId" });
            DropTable("PosCloud.ProductsSubs");
        }
    }
}
