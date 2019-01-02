namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaxIp : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.Taxes", "IsPercentage", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("PosCloud.Taxes", "IsPercentage");
        }
    }
}
