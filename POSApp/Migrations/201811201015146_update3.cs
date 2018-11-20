namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.TimedEvents", "IsPercentage", c => c.Boolean(nullable: false));
            AddColumn("PosCloud.TimedEvents", "IsTaxable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("PosCloud.TimedEvents", "IsTaxable");
            DropColumn("PosCloud.TimedEvents", "IsPercentage");
        }
    }
}
