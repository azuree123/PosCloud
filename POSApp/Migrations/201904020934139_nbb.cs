namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nbb : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.ModifierOptions", "CreatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("PosCloud.ModifierOptions", "CreatedById", c => c.String());
            AddColumn("PosCloud.ModifierOptions", "UpdatedById", c => c.String());
            AddColumn("PosCloud.ModifierOptions", "UpdatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("PosCloud.ModifierOptions", "Synced", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.ModifierOptions", "SyncedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("PosCloud.ModifierOptions", "Code", c => c.String());
            AddColumn("PosCloud.ModifierOptions", "IsDisabled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("PosCloud.ModifierOptions", "IsDisabled");
            DropColumn("PosCloud.ModifierOptions", "Code");
            DropColumn("PosCloud.ModifierOptions", "SyncedOn");
            DropColumn("PosCloud.ModifierOptions", "Synced");
            DropColumn("PosCloud.ModifierOptions", "UpdatedOn");
            DropColumn("PosCloud.ModifierOptions", "UpdatedById");
            DropColumn("PosCloud.ModifierOptions", "CreatedById");
            DropColumn("PosCloud.ModifierOptions", "CreatedOn");
        }
    }
}
