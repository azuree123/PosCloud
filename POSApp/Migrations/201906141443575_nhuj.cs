namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nhuj : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.TransDetails", "EmployeeId", c => c.Int());
            CreateIndex("PosCloud.TransDetails", new[] { "EmployeeId", "StoreId" });
            AddForeignKey("PosCloud.TransDetails", new[] { "EmployeeId", "StoreId" }, "PosCloud.Employees", new[] { "Id", "StoreId" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("PosCloud.TransDetails", new[] { "EmployeeId", "StoreId" }, "PosCloud.Employees");
            DropIndex("PosCloud.TransDetails", new[] { "EmployeeId", "StoreId" });
            DropColumn("PosCloud.TransDetails", "EmployeeId");
        }
    }
}
