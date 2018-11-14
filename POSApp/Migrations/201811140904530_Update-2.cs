namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "PosCloud.Shifts",
                c => new
                    {
                        ShiftId = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 10, unicode: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                    })
                .PrimaryKey(t => new { t.ShiftId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "PosCloud.TillOperations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        OperationDate = c.DateTime(nullable: false),
                        Remarks = c.String(maxLength: 150, unicode: false),
                        OpeningAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SystemAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PhysicalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        ShiftId = c.Int(),
                        Status = c.Boolean(),
                        TillOperationType = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("PosCloud.Shifts", t => new { t.ShiftId, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => new { t.ShiftId, t.StoreId })
                .Index(t => t.ApplicationUserId);
            
            AddColumn("dbo.AspNetUsers", "ShiftId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", new[] { "ShiftId", "StoreId" });
            AddForeignKey("dbo.AspNetUsers", new[] { "ShiftId", "StoreId" }, "PosCloud.Shifts", new[] { "ShiftId", "StoreId" });
        }
        
        public override void Down()
        {
            DropForeignKey("PosCloud.TillOperations", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.TillOperations", new[] { "ShiftId", "StoreId" }, "PosCloud.Shifts");
            DropForeignKey("PosCloud.TillOperations", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("PosCloud.Shifts", "StoreId", "PosCloud.Stores");
            DropForeignKey("dbo.AspNetUsers", new[] { "ShiftId", "StoreId" }, "PosCloud.Shifts");
            DropIndex("PosCloud.TillOperations", new[] { "ApplicationUserId" });
            DropIndex("PosCloud.TillOperations", new[] { "ShiftId", "StoreId" });
            DropIndex("PosCloud.Shifts", new[] { "StoreId" });
            DropIndex("dbo.AspNetUsers", new[] { "ShiftId", "StoreId" });
            DropColumn("dbo.AspNetUsers", "ShiftId");
            DropTable("PosCloud.TillOperations");
            DropTable("PosCloud.Shifts");
        }
    }
}
