namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appcounter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppCounters", "TransferId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppCounters", "TransferId");
        }
    }
}
