namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deviceId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AppCounters", new[] { "Device_Id", "Device_StoreId" }, "PosCloud.Devices");
            DropIndex("dbo.AppCounters", new[] { "Device_Id", "Device_StoreId" });
            AddColumn("PosCloud.TransMaster", "DeviceId", c => c.Int());
            CreateIndex("PosCloud.TransMaster", new[] { "DeviceId", "StoreId" });
            AddForeignKey("PosCloud.TransMaster", new[] { "DeviceId", "StoreId" }, "PosCloud.Devices", new[] { "Id", "StoreId" });
            DropColumn("dbo.AppCounters", "DeviceId");
            DropColumn("dbo.AppCounters", "Device_Id");
            DropColumn("dbo.AppCounters", "Device_StoreId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppCounters", "Device_StoreId", c => c.Int());
            AddColumn("dbo.AppCounters", "Device_Id", c => c.Int());
            AddColumn("dbo.AppCounters", "DeviceId", c => c.Int());
            DropForeignKey("PosCloud.TransMaster", new[] { "DeviceId", "StoreId" }, "PosCloud.Devices");
            DropIndex("PosCloud.TransMaster", new[] { "DeviceId", "StoreId" });
            DropColumn("PosCloud.TransMaster", "DeviceId");
            CreateIndex("dbo.AppCounters", new[] { "Device_Id", "Device_StoreId" });
            AddForeignKey("dbo.AppCounters", new[] { "Device_Id", "Device_StoreId" }, "PosCloud.Devices", new[] { "Id", "StoreId" });
        }
    }
}
