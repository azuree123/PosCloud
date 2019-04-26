namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shiftinemployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.Employees", "ShiftId", c => c.Int(nullable: false,defaultValue:2));
            CreateIndex("PosCloud.Employees", new[] { "ShiftId", "StoreId" });
            AddForeignKey("PosCloud.Employees", new[] { "ShiftId", "StoreId" }, "PosCloud.Shifts", new[] { "ShiftId", "StoreId" });
        }
        
        public override void Down()
        {
            DropForeignKey("PosCloud.Employees", new[] { "ShiftId", "StoreId" }, "PosCloud.Shifts");
            DropIndex("PosCloud.Employees", new[] { "ShiftId", "StoreId" });
            DropColumn("PosCloud.Employees", "ShiftId");
        }
    }
}
