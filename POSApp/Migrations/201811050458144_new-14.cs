namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "PosCloud.TimedEventProducts",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        StoreId = c.Int(nullable: false),
                        TimedEventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.StoreId, t.TimedEventId })
                .ForeignKey("PosCloud.Products", t => new { t.StoreId, t.ProductId }, cascadeDelete: true)
                .ForeignKey("PosCloud.Stores", t => t.StoreId, cascadeDelete: true)
                .ForeignKey("PosCloud.TimedEvents", t => new { t.StoreId, t.TimedEventId }, cascadeDelete: true)
                .Index(t => new { t.StoreId, t.ProductId })
                .Index(t => new { t.StoreId, t.TimedEventId });
            
            
        }
        
        public override void Down()
        {
            CreateTable(
                "PosCloud.TimedEventProducts",
                c => new
                    {
                        TimedEventId = c.Int(nullable: false),
                        TimedEventStoreId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ProductStoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TimedEventId, t.TimedEventStoreId, t.ProductId, t.ProductStoreId });
            
            DropForeignKey("PosCloud.TimedEventProducts", new[] { "StoreId", "TimedEventId" }, "PosCloud.TimedEvents");
            DropForeignKey("PosCloud.TimedEventProducts", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TimedEventProducts", new[] { "StoreId", "ProductId" }, "PosCloud.Products");
            DropIndex("PosCloud.TimedEventProducts", new[] { "StoreId", "TimedEventId" });
            DropIndex("PosCloud.TimedEventProducts", new[] { "StoreId", "ProductId" });
            DropTable("PosCloud.TimedEventProducts");
            CreateIndex("PosCloud.TimedEventProducts", new[] { "ProductId", "ProductStoreId" });
            CreateIndex("PosCloud.TimedEventProducts", new[] { "TimedEventId", "TimedEventStoreId" });
            AddForeignKey("PosCloud.TimedEventProducts", new[] { "ProductId", "ProductStoreId" }, "PosCloud.TimedEvents", new[] { "Id", "StoreId" }, cascadeDelete: true);
            AddForeignKey("PosCloud.TimedEventProducts", new[] { "TimedEventId", "TimedEventStoreId" }, "PosCloud.Products", new[] { "Id", "StoreId" }, cascadeDelete: true);
        }
    }
}
