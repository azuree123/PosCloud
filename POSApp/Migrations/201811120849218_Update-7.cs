namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetRoles", "StoreId", c => c.Int());
            AddColumn("dbo.AspNetRoles", "CreatedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AspNetRoles", "CreatedById", c => c.String());
            AddColumn("dbo.AspNetRoles", "UpdatedById", c => c.String());
            AddColumn("dbo.AspNetRoles", "UpdatedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AspNetRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.AspNetRoles", "StoreId");
            AddForeignKey("dbo.AspNetRoles", "StoreId", "PosCloud.Stores", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetRoles", "StoreId", "PosCloud.Stores");
            DropIndex("dbo.AspNetRoles", new[] { "StoreId" });
            DropColumn("dbo.AspNetRoles", "Discriminator");
            DropColumn("dbo.AspNetRoles", "UpdatedOn");
            DropColumn("dbo.AspNetRoles", "UpdatedById");
            DropColumn("dbo.AspNetRoles", "CreatedById");
            DropColumn("dbo.AspNetRoles", "CreatedOn");
            DropColumn("dbo.AspNetRoles", "StoreId");
        }
    }
}
