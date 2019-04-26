namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usershiftemployee : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", new[] { "ShiftId", "StoreId" }, "PosCloud.Shifts");
            DropIndex("dbo.AspNetUsers", new[] { "ShiftId", "StoreId" });
            RenameColumn(table: "dbo.AspNetUsers", name: "ShiftId", newName: "Shift_ShiftId");
            AddColumn("dbo.AspNetUsers", "Shift_StoreId", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "EmployeeId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", new[] { "Shift_ShiftId", "Shift_StoreId" });
            AddForeignKey("dbo.AspNetUsers", new[] { "Shift_ShiftId", "Shift_StoreId" }, "PosCloud.Shifts", new[] { "ShiftId", "StoreId" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", new[] { "Shift_ShiftId", "Shift_StoreId" }, "PosCloud.Shifts");
            DropIndex("dbo.AspNetUsers", new[] { "Shift_ShiftId", "Shift_StoreId" });
            AlterColumn("dbo.AspNetUsers", "EmployeeId", c => c.Int());
            DropColumn("dbo.AspNetUsers", "Shift_StoreId");
            RenameColumn(table: "dbo.AspNetUsers", name: "Shift_ShiftId", newName: "ShiftId");
            CreateIndex("dbo.AspNetUsers", new[] { "ShiftId", "StoreId" });
            AddForeignKey("dbo.AspNetUsers", new[] { "ShiftId", "StoreId" }, "PosCloud.Shifts", new[] { "ShiftId", "StoreId" });
        }
    }
}
