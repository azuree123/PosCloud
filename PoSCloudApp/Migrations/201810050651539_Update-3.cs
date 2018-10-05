namespace PoSCloudApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Expenses", "ExpenseHeadId", c => c.Int(nullable: false));
            CreateIndex("dbo.Expenses", "ExpenseHeadId");
            CreateIndex("dbo.Expenses", "EmployeeId");
            AddForeignKey("dbo.Expenses", "EmployeeId", "dbo.Employees", "Id");
            AddForeignKey("dbo.Expenses", "ExpenseHeadId", "dbo.ExpenseHeads", "Id");
            DropColumn("dbo.Expenses", "ExpenseHead");
            DropColumn("dbo.Expenses", "PurchaseId");
            DropColumn("dbo.Expenses", "VoucherId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Expenses", "VoucherId", c => c.Int(nullable: false));
            AddColumn("dbo.Expenses", "PurchaseId", c => c.Int(nullable: false));
            AddColumn("dbo.Expenses", "ExpenseHead", c => c.Int(nullable: false));
            DropForeignKey("dbo.Expenses", "ExpenseHeadId", "dbo.ExpenseHeads");
            DropForeignKey("dbo.Expenses", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Expenses", new[] { "EmployeeId" });
            DropIndex("dbo.Expenses", new[] { "ExpenseHeadId" });
            DropColumn("dbo.Expenses", "ExpenseHeadId");
        }
    }
}
