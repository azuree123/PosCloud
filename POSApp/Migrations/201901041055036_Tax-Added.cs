namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaxAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.TransMaster", "Tax", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("PosCloud.TransDetails", "Tax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("PosCloud.TransDetails", "Tax");
            DropColumn("PosCloud.TransMaster", "Tax");
        }
    }
}
