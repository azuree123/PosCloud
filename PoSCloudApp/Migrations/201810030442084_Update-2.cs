namespace PoSCloudApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StateId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        Synced = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("PosCloud.States", t => t.StateId)
                .Index(t => t.StateId);
            
            AddColumn("PosCloud.States", "Synced", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cities", "StateId", "PosCloud.States");
            DropIndex("dbo.Cities", new[] { "StateId" });
            DropColumn("PosCloud.States", "Synced");
            DropTable("dbo.Cities");
        }
    }
}
