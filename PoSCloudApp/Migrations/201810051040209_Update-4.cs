namespace PoSCloudApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update4 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Employees", "DepartmentId");
            CreateIndex("dbo.Employees", "DesignationId");
            AddForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments", "Id");
            AddForeignKey("dbo.Employees", "DesignationId", "dbo.Designations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "DesignationId", "dbo.Designations");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Employees", new[] { "DesignationId" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
        }
    }
}
