namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tras : DbMigration
    {
        public override void Up()
        {
            AlterColumn("PosCloud.TransDetails", "Quantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("PosCloud.TransDetails", "Quantity", c => c.Int(nullable: false));
        }
    }
}
