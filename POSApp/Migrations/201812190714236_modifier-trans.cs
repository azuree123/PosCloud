namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifiertrans : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "PosCloud.ModifierTransDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        TransDetailId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ModifierOptionId = c.Int(nullable: false),
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
                .ForeignKey("PosCloud.ModifierOptions", t => new { t.ModifierOptionId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .ForeignKey("PosCloud.TransDetails", t => new { t.TransDetailId, t.StoreId })
                .Index(t => new { t.ModifierOptionId, t.StoreId })
                .Index(t => new { t.TransDetailId, t.StoreId });
            
        }
        
        public override void Down()
        {
            DropForeignKey("PosCloud.ModifierTransDetails", new[] { "TransDetailId", "StoreId" }, "PosCloud.TransDetails");
            DropForeignKey("PosCloud.ModifierTransDetails", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.ModifierTransDetails", new[] { "ModifierOptionId", "StoreId" }, "PosCloud.ModifierOptions");
            DropIndex("PosCloud.ModifierTransDetails", new[] { "TransDetailId", "StoreId" });
            DropIndex("PosCloud.ModifierTransDetails", new[] { "ModifierOptionId", "StoreId" });
            DropTable("PosCloud.ModifierTransDetails");
        }
    }
}
