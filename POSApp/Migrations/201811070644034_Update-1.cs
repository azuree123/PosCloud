namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("PosCloud.TimedEventProducts", new[] { "ProductId", "StoreId" }, "PosCloud.Products");
            DropForeignKey("PosCloud.TimedEventProducts", new[] { "TimedEventId", "StoreId" }, "PosCloud.TimedEvents");
            AddForeignKey("PosCloud.TimedEventProducts", new[] { "ProductId", "StoreId" }, "PosCloud.Products", new[] { "Id", "StoreId" }, cascadeDelete: true);
            AddForeignKey("PosCloud.TimedEventProducts", new[] { "TimedEventId", "StoreId" }, "PosCloud.TimedEvents", new[] { "Id", "StoreId" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("PosCloud.TimedEventProducts", new[] { "TimedEventId", "StoreId" }, "PosCloud.TimedEvents");
            DropForeignKey("PosCloud.TimedEventProducts", new[] { "ProductId", "StoreId" }, "PosCloud.Products");
            AddForeignKey("PosCloud.TimedEventProducts", new[] { "TimedEventId", "StoreId" }, "PosCloud.TimedEvents", new[] { "Id", "StoreId" });
            AddForeignKey("PosCloud.TimedEventProducts", new[] { "ProductId", "StoreId" }, "PosCloud.Products", new[] { "Id", "StoreId" });
        }
    }
}
