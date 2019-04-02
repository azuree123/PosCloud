namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tn : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.TransMaster", "Name", c => c.String(maxLength: 25, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("PosCloud.TransMaster", "Name");
        }
    }
}
