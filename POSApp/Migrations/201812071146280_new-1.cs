namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "PosCloud.TransMasterPaymentMethods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Method = c.String(maxLength: 150, unicode: false),
                        Amount = c.Double(nullable: false),
                        TransMasterId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .ForeignKey("PosCloud.TransMaster", t => new { t.TransMasterId, t.StoreId })
                .Index(t => t.StoreId)
                .Index(t => new { t.TransMasterId, t.StoreId });
            
        }
        
        public override void Down()
        {
            DropForeignKey("PosCloud.TransMasterPaymentMethods", new[] { "TransMasterId", "StoreId" }, "PosCloud.TransMaster");
            DropForeignKey("PosCloud.TransMasterPaymentMethods", "StoreId", "PosCloud.Stores");
            DropIndex("PosCloud.TransMasterPaymentMethods", new[] { "TransMasterId", "StoreId" });
            DropIndex("PosCloud.TransMasterPaymentMethods", new[] { "StoreId" });
            DropTable("PosCloud.TransMasterPaymentMethods");
        }
    }
}
