namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Istrans : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.TransMaster", "IsPurchased", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("PosCloud.TransMaster", "IsPurchased");
        }
    }
}
