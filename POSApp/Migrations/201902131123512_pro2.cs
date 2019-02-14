namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pro2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("PosCloud.Recipes", new[] { "UnitId", "StoreId" }, "PosCloud.Units");
            DropIndex("PosCloud.Recipes", new[] { "UnitId", "StoreId" });
            DropColumn("PosCloud.Recipes", "UnitId");
        }
        
        public override void Down()
        {
            AddColumn("PosCloud.Recipes", "UnitId", c => c.Int(nullable: false));
            CreateIndex("PosCloud.Recipes", new[] { "UnitId", "StoreId" });
            AddForeignKey("PosCloud.Recipes", new[] { "UnitId", "StoreId" }, "PosCloud.Units", new[] { "Id", "StoreId" });
        }
    }
}
