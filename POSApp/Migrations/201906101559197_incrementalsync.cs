namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class incrementalsync : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IncrementalSyncronizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastSynced = c.DateTime(nullable: false),
                        Licence = c.String(),
                        StoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("PosCloud.Stores", t => t.StoreId)
                .Index(t => t.StoreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IncrementalSyncronizations", "StoreId", "PosCloud.Stores");
            DropIndex("dbo.IncrementalSyncronizations", new[] { "StoreId" });
            DropTable("dbo.IncrementalSyncronizations");
        }
    }
}
