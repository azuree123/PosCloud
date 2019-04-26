namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class path : DbMigration
    {
        public override void Up()
        {
            AlterColumn("PosCloud.ReportLogs", "Details", c => c.String(maxLength: 4000));
            AlterColumn("PosCloud.ReportLogs", "Path", c => c.String(nullable: false, maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("PosCloud.ReportLogs", "Path", c => c.String(nullable: false, maxLength: 8000, unicode: false));
            AlterColumn("PosCloud.ReportLogs", "Details", c => c.String(maxLength: 8000, unicode: false));
        }
    }
}
