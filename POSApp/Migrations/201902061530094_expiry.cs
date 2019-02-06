namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class expiry : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.Recipes", "ExpiryDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("PosCloud.Recipes", "ExpiryDate");
        }
    }
}
