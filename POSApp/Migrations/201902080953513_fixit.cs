namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixit : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "PosCloud.ProductModifiers", newName: "ModifierProducts");
            MoveTable(name: "PosCloud.ModifierProducts", newSchema: "dbo");
            RenameColumn(table: "dbo.ModifierProducts", name: "ProductCode", newName: "Modifier_Id");
            RenameColumn(table: "dbo.ModifierProducts", name: "ProductStoreCode", newName: "Modifier_StoreId");
            RenameColumn(table: "dbo.ModifierProducts", name: "ModifierId", newName: "Product_ProductCode");
            RenameColumn(table: "dbo.ModifierProducts", name: "ModifierStoreId", newName: "Product_StoreId");
            RenameIndex(table: "dbo.ModifierProducts", name: "IX_ProductCode_ProductStoreCode", newName: "IX_Modifier_Id_Modifier_StoreId");
            RenameIndex(table: "dbo.ModifierProducts", name: "IX_ModifierId_ModifierStoreId", newName: "IX_Product_ProductCode_Product_StoreId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ModifierProducts", name: "IX_Product_ProductCode_Product_StoreId", newName: "IX_ModifierId_ModifierStoreId");
            RenameIndex(table: "dbo.ModifierProducts", name: "IX_Modifier_Id_Modifier_StoreId", newName: "IX_ProductCode_ProductStoreCode");
            RenameColumn(table: "dbo.ModifierProducts", name: "Product_StoreId", newName: "ModifierStoreId");
            RenameColumn(table: "dbo.ModifierProducts", name: "Product_ProductCode", newName: "ModifierId");
            RenameColumn(table: "dbo.ModifierProducts", name: "Modifier_StoreId", newName: "ProductStoreCode");
            RenameColumn(table: "dbo.ModifierProducts", name: "Modifier_Id", newName: "ProductCode");
            MoveTable(name: "dbo.ModifierProducts", newSchema: "PosCloud");
            RenameTable(name: "PosCloud.ModifierProducts", newName: "ProductModifiers");
        }
    }
}
