namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("PosCloud.Devices", "City", c => c.String(maxLength: 150, unicode: false));
            AlterColumn("PosCloud.Devices", "State", c => c.String(maxLength: 150, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("PosCloud.Devices", "State", c => c.String(nullable: false, maxLength: 150, unicode: false));
            AlterColumn("PosCloud.Devices", "City", c => c.String(nullable: false, maxLength: 150, unicode: false));
        }
    }
}
