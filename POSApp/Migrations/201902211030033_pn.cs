namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppCounters", "STId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppCounters", "STId");
        }
    }
}
