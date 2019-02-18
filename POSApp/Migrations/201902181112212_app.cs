namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class app : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppCounters", "MifId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppCounters", "MifId");
        }
    }
}
