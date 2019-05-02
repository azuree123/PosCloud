namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mi : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LNK_ROLE_PERMISSION", "Permission_Id", "dbo.PERMISSIONS");
            DropForeignKey("dbo.LNK_ROLE_PERMISSION", "Role_Id", "dbo.ROLES");
            DropForeignKey("dbo.PERMISSIONS", "StoreId", "PosCloud.Stores");
            DropForeignKey("dbo.ROLES", "StoreId", "PosCloud.Stores");
            DropForeignKey("dbo.USERS", "StoreId", "PosCloud.Stores");
            DropForeignKey("dbo.LNK_USER_ROLE", "Role_Id", "dbo.ROLES");
            DropForeignKey("dbo.LNK_USER_ROLE", "User_Id", "dbo.USERS");
            DropIndex("dbo.ROLES", new[] { "StoreId" });
            DropIndex("dbo.PERMISSIONS", new[] { "StoreId" });
            DropIndex("dbo.USERS", new[] { "StoreId" });
            DropIndex("dbo.LNK_ROLE_PERMISSION", new[] { "Permission_Id" });
            DropIndex("dbo.LNK_ROLE_PERMISSION", new[] { "Role_Id" });
            DropIndex("dbo.LNK_USER_ROLE", new[] { "Role_Id" });
            DropIndex("dbo.LNK_USER_ROLE", new[] { "User_Id" });
            DropTable("dbo.ROLES");
            DropTable("dbo.PERMISSIONS");
            DropTable("dbo.USERS");
            DropTable("dbo.LNK_ROLE_PERMISSION");
            DropTable("dbo.LNK_USER_ROLE");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LNK_USER_ROLE",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.User_Id });
            
            CreateTable(
                "dbo.LNK_ROLE_PERMISSION",
                c => new
                    {
                        Permission_Id = c.Int(nullable: false),
                        Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Permission_Id, t.Role_Id });
            
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
                .PrimaryKey(t => t.User_Id);
            
            CreateTable(
                "dbo.PERMISSIONS",
                c => new
                    {
                        Permission_Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(),
                        PermissionDescription = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Permission_Id);
            
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
                .PrimaryKey(t => t.Role_Id);
            
            CreateIndex("dbo.LNK_USER_ROLE", "User_Id");
            CreateIndex("dbo.LNK_USER_ROLE", "Role_Id");
            CreateIndex("dbo.LNK_ROLE_PERMISSION", "Role_Id");
            CreateIndex("dbo.LNK_ROLE_PERMISSION", "Permission_Id");
            CreateIndex("dbo.USERS", "StoreId");
            CreateIndex("dbo.PERMISSIONS", "StoreId");
            CreateIndex("dbo.ROLES", "StoreId");
            AddForeignKey("dbo.LNK_USER_ROLE", "User_Id", "dbo.USERS", "User_Id", cascadeDelete: true);
            AddForeignKey("dbo.LNK_USER_ROLE", "Role_Id", "dbo.ROLES", "Role_Id", cascadeDelete: true);
            AddForeignKey("dbo.USERS", "StoreId", "PosCloud.Stores", "Id");
            AddForeignKey("dbo.ROLES", "StoreId", "PosCloud.Stores", "Id");
            AddForeignKey("dbo.PERMISSIONS", "StoreId", "PosCloud.Stores", "Id");
            AddForeignKey("dbo.LNK_ROLE_PERMISSION", "Role_Id", "dbo.ROLES", "Role_Id", cascadeDelete: true);
            AddForeignKey("dbo.LNK_ROLE_PERMISSION", "Permission_Id", "dbo.PERMISSIONS", "Permission_Id", cascadeDelete: true);
        }
    }
}
