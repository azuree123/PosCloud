namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Session : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.TransMaster", "SessionCode", c => c.Int());
            AddColumn("PosCloud.TillOperations", "SessionCode", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("PosCloud.TillOperations", "SessionCode");
            DropColumn("PosCloud.TransMaster", "SessionCode");
        }
    }
}
