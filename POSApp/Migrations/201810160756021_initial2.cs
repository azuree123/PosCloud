namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "StoreId" });
            AlterColumn("dbo.AspNetUsers", "StoreId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "StoreId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "StoreId" });
            AlterColumn("dbo.AspNetUsers", "StoreId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "StoreId");
        }
    }
}
