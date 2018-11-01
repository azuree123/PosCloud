namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new10 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("PosCloud.Expenses", new[] { "EmployeeId", "StoreId" }, "PosCloud.Employees");
            DropForeignKey("PosCloud.Expenses", new[] { "ExpenseHeadId", "StoreId" }, "PosCloud.ExpenseHeads");
            AddForeignKey("PosCloud.Expenses", new[] { "EmployeeId", "StoreId" }, "PosCloud.Employees", new[] { "Id", "StoreId" }, cascadeDelete: true);
            AddForeignKey("PosCloud.Expenses", new[] { "ExpenseHeadId", "StoreId" }, "PosCloud.ExpenseHeads", new[] { "Id", "StoreId" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("PosCloud.Expenses", new[] { "ExpenseHeadId", "StoreId" }, "PosCloud.ExpenseHeads");
            DropForeignKey("PosCloud.Expenses", new[] { "EmployeeId", "StoreId" }, "PosCloud.Employees");
            AddForeignKey("PosCloud.Expenses", new[] { "ExpenseHeadId", "StoreId" }, "PosCloud.ExpenseHeads", new[] { "Id", "StoreId" });
            AddForeignKey("PosCloud.Expenses", new[] { "EmployeeId", "StoreId" }, "PosCloud.Employees", new[] { "Id", "StoreId" });
        }
    }
}
