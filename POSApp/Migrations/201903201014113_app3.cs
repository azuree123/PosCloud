namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class app3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppCounters", "OpeningStockId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppCounters", "OpeningStockId");
        }
    }
}
