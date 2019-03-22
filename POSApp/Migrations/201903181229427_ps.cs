namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ps : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.ProductsSubs", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("PosCloud.ProductsSubs", "Modifiable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("PosCloud.ProductsSubs", "Modifiable");
            DropColumn("PosCloud.ProductsSubs", "Price");
        }
    }
}
