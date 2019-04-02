namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hgy : DbMigration
    {
        public override void Up()
        {
            DropIndex("PosCloud.Warehouses", new[] { "StoreId" });
            RenameColumn(table: "PosCloud.Warehouses", name: "StoreId", newName: "Store_Id");
            AddColumn("PosCloud.TransMaster", "WarehouseId", c => c.Int());
            AddColumn("PosCloud.Warehouses", "ClientId", c => c.Int(nullable: false));
            AlterColumn("PosCloud.Warehouses", "Store_Id", c => c.Int());
            CreateIndex("PosCloud.TransMaster", "WarehouseId");
            CreateIndex("PosCloud.Warehouses", "ClientId");
            CreateIndex("PosCloud.Warehouses", "Store_Id");
            AddForeignKey("PosCloud.Warehouses", "ClientId", "PosCloud.Clients", "Id");
            AddForeignKey("PosCloud.TransMaster", "WarehouseId", "PosCloud.Warehouses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("PosCloud.TransMaster", "WarehouseId", "PosCloud.Warehouses");
            DropForeignKey("PosCloud.Warehouses", "ClientId", "PosCloud.Clients");
            DropIndex("PosCloud.Warehouses", new[] { "Store_Id" });
            DropIndex("PosCloud.Warehouses", new[] { "ClientId" });
            DropIndex("PosCloud.TransMaster", new[] { "WarehouseId" });
            AlterColumn("PosCloud.Warehouses", "Store_Id", c => c.Int(nullable: false));
            DropColumn("PosCloud.Warehouses", "ClientId");
            DropColumn("PosCloud.TransMaster", "WarehouseId");
            RenameColumn(table: "PosCloud.Warehouses", name: "Store_Id", newName: "StoreId");
            CreateIndex("PosCloud.Warehouses", "StoreId");
        }
    }
}
