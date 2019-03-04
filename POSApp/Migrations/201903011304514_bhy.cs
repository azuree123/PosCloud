namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bhy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppCounters", "StoreId", c => c.Int());
            AddColumn("PosCloud.TransMaster", "ToStoreId", c => c.Int());
            CreateIndex("dbo.AppCounters", "StoreId");
            AddForeignKey("dbo.AppCounters", "StoreId", "PosCloud.Stores", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppCounters", "StoreId", "PosCloud.Stores");
            DropIndex("dbo.AppCounters", new[] { "StoreId" });
            DropColumn("PosCloud.TransMaster", "ToStoreId");
            DropColumn("dbo.AppCounters", "StoreId");
        }
    }
}
