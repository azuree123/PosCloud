namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.Floors", "CreatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("PosCloud.Floors", "CreatedById", c => c.String());
            AddColumn("PosCloud.Floors", "UpdatedById", c => c.String());
            AddColumn("PosCloud.Floors", "UpdatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("PosCloud.Floors", "Synced", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.Floors", "SyncedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("PosCloud.Floors", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("PosCloud.Floors", "Code");
            DropColumn("PosCloud.Floors", "SyncedOn");
            DropColumn("PosCloud.Floors", "Synced");
            DropColumn("PosCloud.Floors", "UpdatedOn");
            DropColumn("PosCloud.Floors", "UpdatedById");
            DropColumn("PosCloud.Floors", "CreatedById");
            DropColumn("PosCloud.Floors", "CreatedOn");
        }
    }
}
