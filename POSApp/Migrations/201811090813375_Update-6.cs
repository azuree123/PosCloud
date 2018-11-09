namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("PosCloud.TransDetails", new[] { "ProductId", "StoreId" }, "PosCloud.Products");
            AddForeignKey("PosCloud.TransDetails", new[] { "ProductId", "StoreId" }, "PosCloud.Products", new[] { "Id", "StoreId" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("PosCloud.TransDetails", new[] { "ProductId", "StoreId" }, "PosCloud.Products");
            AddForeignKey("PosCloud.TransDetails", new[] { "ProductId", "StoreId" }, "PosCloud.Products", new[] { "Id", "StoreId" });
        }
    }
}
