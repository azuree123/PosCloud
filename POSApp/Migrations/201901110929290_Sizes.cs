namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sizes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "PosCloud.Sizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
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
                .Index(t => t.StoreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("PosCloud.Sizes", "StoreId", "PosCloud.Stores");
            DropIndex("PosCloud.Sizes", new[] { "StoreId" });
            DropTable("PosCloud.Sizes");
        }
    }
}
