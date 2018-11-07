namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("PosCloud.Coupons", "StoreId", "PosCloud.Stores");
            DropIndex("PosCloud.Coupons", new[] { "StoreId" });
            CreateTable(
                "PosCloud.Devices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        License = c.String(nullable: false, maxLength: 150, unicode: false),
                        DeviceCode = c.String(nullable: false, maxLength: 150, unicode: false),
                        AppVersion = c.String(nullable: false, maxLength: 150, unicode: false),
                        DownloadedDate = c.DateTime(),
                        Address = c.String(nullable: false, maxLength: 150, unicode: false),
                        Contact = c.String(nullable: false, maxLength: 150, unicode: false),
                        City = c.String(nullable: false, maxLength: 150, unicode: false),
                        State = c.String(nullable: false, maxLength: 150, unicode: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId })
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            AddColumn("PosCloud.Discounts", "Type", c => c.String(nullable: false, maxLength: 150, unicode: false));
            AddColumn("PosCloud.Discounts", "DiscountCode", c => c.String(nullable: false, maxLength: 150, unicode: false));
            AddColumn("PosCloud.Discounts", "Value", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("PosCloud.Discounts", "ValidFrom", c => c.DateTime());
            AddColumn("PosCloud.Discounts", "ValidTill", c => c.DateTime());
            AddColumn("PosCloud.Discounts", "Days", c => c.String(nullable: false, maxLength: 150, unicode: false));
            AddColumn("PosCloud.Discounts", "IsActive", c => c.Boolean());
            AlterColumn("PosCloud.Discounts", "Code", c => c.String());
            AlterColumn("PosCloud.Clients", "Name", c => c.String());
            DropColumn("PosCloud.Discounts", "Amount");
            DropColumn("PosCloud.Clients", "Image");
            DropTable("PosCloud.Coupons");
        }
        
        public override void Down()
        {
            CreateTable(
                "PosCloud.Coupons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
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
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => new { t.Id, t.StoreId });
            
            AddColumn("PosCloud.Clients", "Image", c => c.String(maxLength: 150));
            AddColumn("PosCloud.Discounts", "Amount", c => c.Double(nullable: false));
            DropForeignKey("PosCloud.Devices", "StoreId", "PosCloud.Stores");
            DropIndex("PosCloud.Devices", new[] { "StoreId" });
            AlterColumn("PosCloud.Clients", "Name", c => c.String(maxLength: 150));
            AlterColumn("PosCloud.Discounts", "Code", c => c.String(maxLength: 150, unicode: false));
            DropColumn("PosCloud.Discounts", "IsActive");
            DropColumn("PosCloud.Discounts", "Days");
            DropColumn("PosCloud.Discounts", "ValidTill");
            DropColumn("PosCloud.Discounts", "ValidFrom");
            DropColumn("PosCloud.Discounts", "Value");
            DropColumn("PosCloud.Discounts", "DiscountCode");
            DropColumn("PosCloud.Discounts", "Type");
            DropTable("PosCloud.Devices");
            CreateIndex("PosCloud.Coupons", "StoreId");
            AddForeignKey("PosCloud.Coupons", "StoreId", "PosCloud.Stores", "Id");
        }
    }
}
