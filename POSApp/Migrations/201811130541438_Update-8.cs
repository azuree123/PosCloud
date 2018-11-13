namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "PosCloud.SecurityRights",
                c => new
                    {
                        IdentityUserRoleId = c.String(nullable: false, maxLength: 128),
                        StoreId = c.Int(nullable: false),
                        SecurityObjectId = c.Int(nullable: false),
                        Manage = c.Boolean(nullable: false),
                        View = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdentityUserRoleId, t.StoreId, t.SecurityObjectId })
                .ForeignKey("dbo.AspNetRoles", t => t.IdentityUserRoleId, cascadeDelete: true)
                .ForeignKey("PosCloud.SecurityObjects", t => t.SecurityObjectId, cascadeDelete: true)
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.IdentityUserRoleId)
                .Index(t => t.StoreId)
                .Index(t => t.SecurityObjectId);
            
            CreateTable(
                "PosCloud.SecurityObjects",
                c => new
                    {
                        SecurityObjectId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 150),
                        Type = c.String(maxLength: 150),
                        Module = c.String(maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.SecurityObjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("PosCloud.SecurityRights", "StoreId", "PosCloud.Stores");
            DropForeignKey("PosCloud.SecurityRights", "SecurityObjectId", "PosCloud.SecurityObjects");
            DropForeignKey("PosCloud.SecurityRights", "IdentityUserRoleId", "dbo.AspNetRoles");
            DropIndex("PosCloud.SecurityRights", new[] { "SecurityObjectId" });
            DropIndex("PosCloud.SecurityRights", new[] { "StoreId" });
            DropIndex("PosCloud.SecurityRights", new[] { "IdentityUserRoleId" });
            DropTable("PosCloud.SecurityObjects");
            DropTable("PosCloud.SecurityRights");
        }
    }
}
