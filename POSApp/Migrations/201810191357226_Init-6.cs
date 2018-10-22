namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init6 : DbMigration
    {
        public override void Up()
        {
            DropIndex("PosCloud.SaleOrderDetails", new[] { "StoreId" });
            DropIndex("PosCloud.Products", new[] { "StoreId" });
            CreateTable(
                "PosCloud.Coupons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        Code = c.String(maxLength: 4, unicode: false),
                        Value = c.Double(nullable: false),
                        ValidFrom = c.DateTime(nullable: false, storeType: "date"),
                        ValidTill = c.DateTime(nullable: false, storeType: "date"),
                        Amount = c.Int(nullable: false),
                        Days = c.String(maxLength: 150, unicode: false),
                        IsPercentage = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("PosCloud.Coupons", "StoreId", "PosCloud.Stores");
            DropIndex("PosCloud.Coupons", new[] { "StoreId" });
            DropTable("PosCloud.Coupons");
            CreateIndex("PosCloud.Products", "StoreId");
            CreateIndex("PosCloud.SaleOrderDetails", "StoreId");
        }
    }
}
