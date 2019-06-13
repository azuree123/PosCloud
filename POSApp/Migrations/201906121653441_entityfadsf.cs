namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class entityfadsf : DbMigration
    {
        public override void Up()
        {
            AlterColumn("PosCloud.IncrementalSyncronizations", "TableName", c => c.String(nullable: false, maxLength: 150, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("PosCloud.IncrementalSyncronizations", "TableName", c => c.String(nullable: false, maxLength: 100, unicode: false));
        }
    }
}
