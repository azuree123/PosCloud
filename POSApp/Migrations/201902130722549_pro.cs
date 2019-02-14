namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pro : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.TransMaster", "Description", c => c.String());
            AddColumn("PosCloud.Products", "PurchaseUnit", c => c.String(maxLength: 300, unicode: false));
            AddColumn("PosCloud.Products", "StorageUnit", c => c.String(maxLength: 300, unicode: false));
            AddColumn("PosCloud.Products", "IngredientUnit", c => c.String(maxLength: 300, unicode: false));
            AddColumn("PosCloud.Products", "PtoSFactor", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("PosCloud.Products", "StoIFactor", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("PosCloud.Products", "StoIFactor");
            DropColumn("PosCloud.Products", "PtoSFactor");
            DropColumn("PosCloud.Products", "IngredientUnit");
            DropColumn("PosCloud.Products", "StorageUnit");
            DropColumn("PosCloud.Products", "PurchaseUnit");
            DropColumn("PosCloud.TransMaster", "Description");
        }
    }
}
