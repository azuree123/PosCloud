namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial4 : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "dbo.Stores", newSchema: "PosCloud");
            AlterColumn("PosCloud.Stores", "Name", c => c.String(nullable: false, maxLength: 150, unicode: false));
            AlterColumn("PosCloud.Stores", "Address", c => c.String(maxLength: 150, unicode: false));
            AlterColumn("PosCloud.Stores", "Contact", c => c.String(maxLength: 150, unicode: false));
            AlterColumn("PosCloud.Stores", "City", c => c.String(maxLength: 150, unicode: false));
            AlterColumn("PosCloud.Stores", "State", c => c.String(maxLength: 150, unicode: false));
            AlterColumn("PosCloud.Stores", "IsOperational", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("PosCloud.Stores", "IsOperational", c => c.Boolean(nullable: false));
            AlterColumn("PosCloud.Stores", "State", c => c.String());
            AlterColumn("PosCloud.Stores", "City", c => c.String());
            AlterColumn("PosCloud.Stores", "Contact", c => c.String());
            AlterColumn("PosCloud.Stores", "Address", c => c.String());
            AlterColumn("PosCloud.Stores", "Name", c => c.String());
            MoveTable(name: "PosCloud.Stores", newSchema: "dbo");
        }
    }
}
