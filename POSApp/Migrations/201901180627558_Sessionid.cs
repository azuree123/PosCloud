namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sessionid : DbMigration
    {
        public override void Up()
        {
            AlterColumn("PosCloud.TransMaster", "SessionCode", c => c.Int(nullable: false));
            AlterColumn("PosCloud.TillOperations", "SessionCode", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("PosCloud.TillOperations", "SessionCode", c => c.Int());
            AlterColumn("PosCloud.TransMaster", "SessionCode", c => c.Int());
        }
    }
}
