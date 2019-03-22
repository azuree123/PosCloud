namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class combosub : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ComboProductsTransDetails", new[] { "Id", "StoreId" }, "PosCloud.TransDetails");
            DropForeignKey("dbo.ComboProductsTransDetails", "StoreId", "PosCloud.Stores");
            DropForeignKey("dbo.ComboProductsTransDetails", new[] { "ProductsSub_ComboProductCode", "ProductsSub_StoreId", "ProductsSub_ProductCode" }, "PosCloud.ProductsSubs");
            DropIndex("dbo.ComboProductsTransDetails", new[] { "ProductsSub_ComboProductCode", "ProductsSub_StoreId", "ProductsSub_ProductCode" });
            DropIndex("dbo.ComboProductsTransDetails", new[] { "StoreId" });
            DropIndex("dbo.ComboProductsTransDetails", new[] { "Id", "StoreId" });
            DropTable("dbo.ComboProductsTransDetails");
        }
    }
}
