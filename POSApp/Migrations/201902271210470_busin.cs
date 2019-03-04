namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class busin : DbMigration
    {
        public override void Up()
        {
            DropIndex("PosCloud.TransMaster", new[] { "BusinessPartnerId", "StoreId" });
            AlterColumn("PosCloud.TransMaster", "BusinessPartnerId", c => c.Int());
            CreateIndex("PosCloud.TransMaster", new[] { "BusinessPartnerId", "StoreId" });
        }
        
        public override void Down()
        {
            DropIndex("PosCloud.TransMaster", new[] { "BusinessPartnerId", "StoreId" });
            AlterColumn("PosCloud.TransMaster", "BusinessPartnerId", c => c.Int(nullable: false));
            CreateIndex("PosCloud.TransMaster", new[] { "BusinessPartnerId", "StoreId" });
        }
    }
}
