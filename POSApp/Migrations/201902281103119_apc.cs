namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class apc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppCounters", "PurchasingId", c => c.Int(nullable: false));
            AddColumn("dbo.AppCounters", "OtherInId", c => c.Int(nullable: false));
            AddColumn("dbo.AppCounters", "OtherOutId", c => c.Int(nullable: false));
            AddColumn("dbo.AppCounters", "ExpiryId", c => c.Int(nullable: false));
            AddColumn("dbo.AppCounters", "DamageId", c => c.Int(nullable: false));
            AddColumn("dbo.AppCounters", "WasteId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppCounters", "WasteId");
            DropColumn("dbo.AppCounters", "DamageId");
            DropColumn("dbo.AppCounters", "ExpiryId");
            DropColumn("dbo.AppCounters", "OtherOutId");
            DropColumn("dbo.AppCounters", "OtherInId");
            DropColumn("dbo.AppCounters", "PurchasingId");
        }
    }
}
