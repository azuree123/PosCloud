namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class increv : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IncrementalSyncronizations", "StoreId", "PosCloud.Stores");
            DropIndex("dbo.IncrementalSyncronizations", new[] { "StoreId" });
            DropPrimaryKey("dbo.IncrementalSyncronizations");
            MoveTable(name: "dbo.IncrementalSyncronizations", newSchema: "PosCloud");
            AddColumn("PosCloud.IncrementalSyncronizations", "DeviceId", c => c.Int(nullable: false));
            AddPrimaryKey("PosCloud.IncrementalSyncronizations", new[] { "Id", "StoreId", "DeviceId" });
            CreateIndex("PosCloud.IncrementalSyncronizations", new[] { "DeviceId", "StoreId" });
            AddForeignKey("PosCloud.IncrementalSyncronizations", new[] { "DeviceId", "StoreId" }, "PosCloud.Devices", new[] { "Id", "StoreId" }, cascadeDelete: true);
            AddForeignKey("PosCloud.IncrementalSyncronizations", "StoreId", "PosCloud.Stores", "Id", cascadeDelete: true);
            DropColumn("PosCloud.IncrementalSyncronizations", "Licence");
        }
        
        public override void Down()
        {
            AddColumn("PosCloud.IncrementalSyncronizations", "Licence", c => c.String());
            DropForeignKey("PosCloud.IncrementalSyncronizations", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.IncrementalSyncronizations", new[] { "DeviceId", "StoreId" }, "PosCloud.Devices");
            DropIndex("PosCloud.IncrementalSyncronizations", new[] { "DeviceId", "StoreId" });
            DropPrimaryKey("PosCloud.IncrementalSyncronizations");
            DropColumn("PosCloud.IncrementalSyncronizations", "DeviceId");
            AddPrimaryKey("PosCloud.IncrementalSyncronizations", "Id");
            CreateIndex("PosCloud.IncrementalSyncronizations", "StoreId");
            AddForeignKey("dbo.IncrementalSyncronizations", "StoreId", "PosCloud.Stores", "Id");
            MoveTable(name: "PosCloud.IncrementalSyncronizations", newSchema: "dbo");
        }
    }
}
