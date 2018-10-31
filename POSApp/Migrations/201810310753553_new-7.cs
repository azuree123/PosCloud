namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("PosCloud.Products", "Name", c => c.String(nullable: false, maxLength: 150, unicode: false));
            AlterColumn("PosCloud.Products", "Description", c => c.String(maxLength: 300, unicode: false));
            AlterColumn("PosCloud.Products", "ProductCode", c => c.String(maxLength: 150, unicode: false));
            AlterColumn("PosCloud.Products", "Attribute", c => c.String(maxLength: 150, unicode: false));
            AlterColumn("PosCloud.Products", "Size", c => c.String(maxLength: 150, unicode: false));
            AlterColumn("PosCloud.Products", "Barcode", c => c.String(maxLength: 150, unicode: false));
            AlterColumn("PosCloud.Products", "Image", c => c.String(maxLength: 150, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("PosCloud.Products", "Image", c => c.String(maxLength: 150));
            AlterColumn("PosCloud.Products", "Barcode", c => c.String(maxLength: 150));
            AlterColumn("PosCloud.Products", "Size", c => c.String());
            AlterColumn("PosCloud.Products", "Attribute", c => c.String());
            AlterColumn("PosCloud.Products", "ProductCode", c => c.String(maxLength: 150));
            AlterColumn("PosCloud.Products", "Description", c => c.String(maxLength: 300));
            AlterColumn("PosCloud.Products", "Name", c => c.String(nullable: false, maxLength: 150));
        }
    }
}
