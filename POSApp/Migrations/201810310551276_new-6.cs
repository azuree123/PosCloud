namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new6 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AppCounters", "CreatedOn");
            DropColumn("dbo.AppCounters", "CreatedById");
            DropColumn("dbo.AppCounters", "UpdatedById");
            DropColumn("dbo.AppCounters", "UpdatedOn");
            DropColumn("dbo.AppCounters", "Synced");
            DropColumn("dbo.AppCounters", "SyncedOn");
            DropColumn("dbo.AppCounters", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppCounters", "Code", c => c.String());
            AddColumn("dbo.AppCounters", "SyncedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AppCounters", "Synced", c => c.Boolean(nullable: false));
            AddColumn("dbo.AppCounters", "UpdatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AppCounters", "UpdatedById", c => c.String());
            AddColumn("dbo.AppCounters", "CreatedById", c => c.String());
            AddColumn("dbo.AppCounters", "CreatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
