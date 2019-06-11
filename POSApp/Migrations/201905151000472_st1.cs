namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class st1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "POSTerminalId", "StoreId" });
            AlterColumn("dbo.AspNetUsers", "StoreId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", new[] { "POSTerminalId", "StoreId" });
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "POSTerminalId", "StoreId" });
            AlterColumn("dbo.AspNetUsers", "StoreId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", new[] { "POSTerminalId", "StoreId" });
        }
    }
}
