namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bhyg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppCounters", "StockTakingId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppCounters", "StockTakingId");
        }
    }
}
