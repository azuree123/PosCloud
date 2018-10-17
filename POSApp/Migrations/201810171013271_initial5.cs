namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("PosCloud.Products", new[] { "CategoryId", "StoreId" }, "PosCloud.ProductCategories");
            AddForeignKey("PosCloud.Products", new[] { "CategoryId", "StoreId" }, "PosCloud.ProductCategories", new[] { "Id", "StoreId" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("PosCloud.Products", new[] { "CategoryId", "StoreId" }, "PosCloud.ProductCategories");
            AddForeignKey("PosCloud.Products", new[] { "CategoryId", "StoreId" }, "PosCloud.ProductCategories", new[] { "Id", "StoreId" });
        }
    }
}
