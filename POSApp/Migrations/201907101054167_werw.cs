namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class werw : DbMigration
    {
        public override void Up()
        {
            AlterColumn("PosCloud.BusinessPartners", "CNICNumber", c => c.String(maxLength: 150, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("PosCloud.BusinessPartners", "CNICNumber", c => c.String());
        }
    }
}
