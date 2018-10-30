namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emp121 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("PosCloud.Employees", "Id", "PosCloud.Designations");
            DropIndex("PosCloud.Employees", new[] { "Id" });
            DropTable("PosCloud.Designations");
        }
        
        public override void Down()
        {
            CreateTable(
                "PosCloud.Designations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Synced = c.Boolean(nullable: false),
                        SyncedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        Code = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("PosCloud.Employees", "Id");
            AddForeignKey("PosCloud.Employees", "Id", "PosCloud.Designations", "Id");
        }
    }
}
