namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tableName : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.IncrementalSyncronizations", "TableName", c => c.String(nullable: false, maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("PosCloud.IncrementalSyncronizations", "TableName");
        }
    }
}
