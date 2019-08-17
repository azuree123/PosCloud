namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cust : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.BusinessPartners", "CNICNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("PosCloud.BusinessPartners", "CNICNumber");
        }
    }
}
