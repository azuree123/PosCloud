namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class waste : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.TransDetails", "Waste", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("PosCloud.TransDetails", "Waste");
        }
    }
}
