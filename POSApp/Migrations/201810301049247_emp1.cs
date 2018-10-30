namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emp1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("PosCloud.Employees", "DepartmentId", "PosCloud.Departments");
            DropIndex("PosCloud.Employees", new[] { "StoreId" });
            DropIndex("PosCloud.Employees", new[] { "DepartmentId" });
            DropPrimaryKey("PosCloud.Departments");
            AddColumn("PosCloud.Departments", "StoreId", c => c.Int(nullable: false));
            AlterColumn("PosCloud.Departments", "Name", c => c.String(nullable: false, maxLength: 150, unicode: false));
            AddPrimaryKey("PosCloud.Departments", new[] { "Id", "StoreId" });
            CreateIndex("PosCloud.Employees", new[] { "DepartmentId", "StoreId" });
            CreateIndex("PosCloud.Departments", "StoreId");
            AddForeignKey("PosCloud.Departments", "StoreId", "PosCloud.Stores", "Id");
            AddForeignKey("PosCloud.Employees", new[] { "DepartmentId", "StoreId" }, "PosCloud.Departments", new[] { "Id", "StoreId" });
        }
        
        public override void Down()
        {
            DropForeignKey("PosCloud.Employees", new[] { "DepartmentId", "StoreId" }, "PosCloud.Departments");
            DropForeignKey("PosCloud.Departments", "StoreId", "PosCloud.Stores");
            DropIndex("PosCloud.Departments", new[] { "StoreId" });
            DropIndex("PosCloud.Employees", new[] { "DepartmentId", "StoreId" });
            DropPrimaryKey("PosCloud.Departments");
            AlterColumn("PosCloud.Departments", "Name", c => c.String(nullable: false, maxLength: 150));
            DropColumn("PosCloud.Departments", "StoreId");
            AddPrimaryKey("PosCloud.Departments", "Id");
            CreateIndex("PosCloud.Employees", "DepartmentId");
            CreateIndex("PosCloud.Employees", "StoreId");
            AddForeignKey("PosCloud.Employees", "DepartmentId", "PosCloud.Departments", "Id");
        }
    }
}
