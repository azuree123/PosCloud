namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pro1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("PosCloud.Products", new[] { "CategoryId", "StoreId" });
            DropIndex("PosCloud.Products", new[] { "UnitId", "StoreId" });
            AlterColumn("PosCloud.Products", "CategoryId", c => c.Int());
            AlterColumn("PosCloud.Products", "UnitId", c => c.Int());
            CreateIndex("PosCloud.Products", new[] { "CategoryId", "StoreId" });
            CreateIndex("PosCloud.Products", new[] { "UnitId", "StoreId" });
        }
        
        public override void Down()
        {
            DropIndex("PosCloud.Products", new[] { "UnitId", "StoreId" });
            DropIndex("PosCloud.Products", new[] { "CategoryId", "StoreId" });
            AlterColumn("PosCloud.Products", "UnitId", c => c.Int(nullable: false));
            AlterColumn("PosCloud.Products", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("PosCloud.Products", new[] { "UnitId", "StoreId" });
            CreateIndex("PosCloud.Products", new[] { "CategoryId", "StoreId" });
        }
    }
}
