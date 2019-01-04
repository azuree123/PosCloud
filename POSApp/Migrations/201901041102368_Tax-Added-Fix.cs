namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaxAddedFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("PosCloud.TransMaster", "Tax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("PosCloud.TransMaster", "Tax", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
