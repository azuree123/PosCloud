namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class client : DbMigration
    {
        public override void Up()
        {
            DropIndex("PosCloud.Stores", new[] { "ClientId" });
            AlterColumn("PosCloud.Stores", "ClientId", c => c.Int(nullable: false));
            CreateIndex("PosCloud.Stores", "ClientId");
        }
        
        public override void Down()
        {
            DropIndex("PosCloud.Stores", new[] { "ClientId" });
            AlterColumn("PosCloud.Stores", "ClientId", c => c.Int());
            CreateIndex("PosCloud.Stores", "ClientId");
        }
    }
}
