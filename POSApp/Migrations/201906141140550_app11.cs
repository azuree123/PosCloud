namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class app11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppCounters", "DeviceId", c => c.Int(nullable: false));
            AddColumn("dbo.AppCounters", "Device_Id", c => c.Int());
            AddColumn("dbo.AppCounters", "Device_StoreId", c => c.Int());
            CreateIndex("dbo.AppCounters", new[] { "Device_Id", "Device_StoreId" });
            AddForeignKey("dbo.AppCounters", new[] { "Device_Id", "Device_StoreId" }, "PosCloud.Devices", new[] { "Id", "StoreId" });
        }

        public override void Down()
        {
            DropForeignKey("dbo.AppCounters", new[] { "Device_Id", "Device_StoreId" }, "PosCloud.Devices");
            DropIndex("dbo.AppCounters", new[] { "Device_Id", "Device_StoreId" });
            DropColumn("dbo.AppCounters", "Device_StoreId");
            DropColumn("dbo.AppCounters", "Device_Id");
            DropColumn("dbo.AppCounters", "DeviceId");
        }
    }
}
