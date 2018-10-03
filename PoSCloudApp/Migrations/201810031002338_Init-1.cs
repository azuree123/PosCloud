namespace PoSCloudApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init1 : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "dbo.Cities", newSchema: "PosCloud");
            DropForeignKey("dbo.Cities", "StateId", "PosCloud.States");
            AlterColumn("PosCloud.Cities", "Name", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("PosCloud.Cities", "CreatedBy", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("PosCloud.Cities", "UpdatedBy", c => c.String(maxLength: 150));
            AddForeignKey("PosCloud.Cities", "StateId", "PosCloud.States", "Id", cascadeDelete: true);
            DropColumn("dbo.AspNetUsers", "UserId");
            DropColumn("dbo.AspNetUsers", "Password");
            DropColumn("dbo.AspNetUsers", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Role", c => c.String());
            AddColumn("dbo.AspNetUsers", "Password", c => c.String());
            AddColumn("dbo.AspNetUsers", "UserId", c => c.Int(nullable: false));
            DropForeignKey("PosCloud.Cities", "StateId", "PosCloud.States");
            AlterColumn("PosCloud.Cities", "UpdatedBy", c => c.String());
            AlterColumn("PosCloud.Cities", "CreatedBy", c => c.String());
            AlterColumn("PosCloud.Cities", "Name", c => c.String());
            AddForeignKey("dbo.Cities", "StateId", "PosCloud.States", "Id");
            MoveTable(name: "PosCloud.Cities", newSchema: "dbo");
        }
    }
}
