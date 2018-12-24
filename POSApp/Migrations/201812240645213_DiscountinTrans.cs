namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DiscountinTrans : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.TransMaster", "DiscountId", c => c.Int());
            AddColumn("PosCloud.TransMaster", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("PosCloud.TransDetails", "DiscountId", c => c.Int());
            CreateIndex("PosCloud.TransMaster", new[] { "DiscountId", "StoreId" });
            CreateIndex("PosCloud.TransDetails", new[] { "DiscountId", "StoreId" });
            AddForeignKey("PosCloud.TransDetails", new[] { "DiscountId", "StoreId" }, "PosCloud.TimedEvents", new[] { "Id", "StoreId" }, cascadeDelete: true);
            AddForeignKey("PosCloud.TransMaster", new[] { "DiscountId", "StoreId" }, "PosCloud.TimedEvents", new[] { "Id", "StoreId" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("PosCloud.TransMaster", new[] { "DiscountId", "StoreId" }, "PosCloud.TimedEvents");
            DropForeignKey("PosCloud.TransDetails", new[] { "DiscountId", "StoreId" }, "PosCloud.TimedEvents");
            DropIndex("PosCloud.TransDetails", new[] { "DiscountId", "StoreId" });
            DropIndex("PosCloud.TransMaster", new[] { "DiscountId", "StoreId" });
            DropColumn("PosCloud.TransDetails", "DiscountId");
            DropColumn("PosCloud.TransMaster", "Discount");
            DropColumn("PosCloud.TransMaster", "DiscountId");
        }
    }
}
