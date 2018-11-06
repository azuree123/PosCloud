namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timedeventfix : DbMigration
    {
        public override void Up()
        {
            DropIndex("PosCloud.TimedEventProducts", new[] { "ProductId", "StoreId" });
            DropIndex("PosCloud.TimedEventProducts", new[] { "TimedEventId", "StoreId" });
            DropIndex("PosCloud.TimedEventProducts", new[] { "Store_Id" });
            DropColumn("PosCloud.TimedEventProducts", "StoreId");
            RenameColumn(table: "PosCloud.TimedEventProducts", name: "Store_Id", newName: "StoreId");
            DropPrimaryKey("PosCloud.TimedEventProducts");
            AlterColumn("PosCloud.TimedEventProducts", "StoreId", c => c.Int(nullable: false));
            AddPrimaryKey("PosCloud.TimedEventProducts", new[] { "ProductId", "TimedEventId", "StoreId" });
            CreateIndex("PosCloud.TimedEventProducts", new[] { "ProductId", "StoreId" });
            CreateIndex("PosCloud.TimedEventProducts", new[] { "TimedEventId", "StoreId" });
        }
        
        public override void Down()
        {
            DropIndex("PosCloud.TimedEventProducts", new[] { "TimedEventId", "StoreId" });
            DropIndex("PosCloud.TimedEventProducts", new[] { "ProductId", "StoreId" });
            DropPrimaryKey("PosCloud.TimedEventProducts");
            AlterColumn("PosCloud.TimedEventProducts", "StoreId", c => c.Int());
            AddPrimaryKey("PosCloud.TimedEventProducts", new[] { "ProductId", "TimedEventId", "StoreId" });
            RenameColumn(table: "PosCloud.TimedEventProducts", name: "StoreId", newName: "Store_Id");
            AddColumn("PosCloud.TimedEventProducts", "StoreId", c => c.Int(nullable: false));
            CreateIndex("PosCloud.TimedEventProducts", "Store_Id");
            CreateIndex("PosCloud.TimedEventProducts", new[] { "TimedEventId", "StoreId" });
            CreateIndex("PosCloud.TimedEventProducts", new[] { "ProductId", "StoreId" });
        }
    }
}
