namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("PosCloud.Products", "Image", c => c.Binary());
            AlterColumn("PosCloud.ProductCategories", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("PosCloud.ProductCategories", "Image", c => c.String(maxLength: 150));
            AlterColumn("PosCloud.Products", "Image", c => c.String(maxLength: 150, unicode: false));
        }
    }
}
