namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ayth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ROLES",
                c => new
                    {
                        Role_Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(),
                        RoleName = c.String(nullable: false),
                        RoleDescription = c.String(),
                        IsSysAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Role_Id)
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "dbo.PERMISSIONS",
                c => new
                    {
                        Permission_Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(),
                        PermissionDescription = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Permission_Id)
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "dbo.USERS",
                c => new
                    {
                        User_Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(),
                        Username = c.String(nullable: false, maxLength: 30),
                        LastModified = c.DateTime(),
                        Inactive = c.Boolean(),
                        Firstname = c.String(maxLength: 50),
                        Lastname = c.String(maxLength: 50),
                        Title = c.String(maxLength: 30),
                        Initial = c.String(maxLength: 3),
                        EMail = c.String(maxLength: 100),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(),
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.User_Id)
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "dbo.LNK_ROLE_PERMISSION",
                c => new
                    {
                        Permission_Id = c.Int(nullable: false),
                        Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Permission_Id, t.Role_Id })
                .ForeignKey("dbo.PERMISSIONS", t => t.Permission_Id, cascadeDelete: true)
                .ForeignKey("dbo.ROLES", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.Permission_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.LNK_USER_ROLE",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.User_Id })
                .ForeignKey("dbo.ROLES", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.USERS", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LNK_USER_ROLE", "User_Id", "dbo.USERS");
            DropForeignKey("dbo.LNK_USER_ROLE", "Role_Id", "dbo.ROLES");
            DropForeignKey("dbo.USERS", "StoreId", "PosCloud.Stores");
            DropForeignKey("dbo.ROLES", "StoreId", "PosCloud.Stores");
            DropForeignKey("dbo.PERMISSIONS", "StoreId", "PosCloud.Stores");
            DropForeignKey("dbo.LNK_ROLE_PERMISSION", "Role_Id", "dbo.ROLES");
            DropForeignKey("dbo.LNK_ROLE_PERMISSION", "Permission_Id", "dbo.PERMISSIONS");
            DropIndex("dbo.LNK_USER_ROLE", new[] { "User_Id" });
            DropIndex("dbo.LNK_USER_ROLE", new[] { "Role_Id" });
            DropIndex("dbo.LNK_ROLE_PERMISSION", new[] { "Role_Id" });
            DropIndex("dbo.LNK_ROLE_PERMISSION", new[] { "Permission_Id" });
            DropIndex("dbo.USERS", new[] { "StoreId" });
            DropIndex("dbo.PERMISSIONS", new[] { "StoreId" });
            DropIndex("dbo.ROLES", new[] { "StoreId" });
            DropTable("dbo.LNK_USER_ROLE");
            DropTable("dbo.LNK_ROLE_PERMISSION");
            DropTable("dbo.USERS");
            DropTable("dbo.PERMISSIONS");
            DropTable("dbo.ROLES");
        }
    }
}
