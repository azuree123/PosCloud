namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appinfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.Stores", "BusinessStartTime", c => c.DateTime(nullable: false));
            AddColumn("PosCloud.Stores", "Currency", c => c.String(maxLength: 150, unicode: false));
            AddColumn("PosCloud.Devices", "ReceiptHeader", c => c.String(nullable: false, maxLength: 150, unicode: false));
            AddColumn("PosCloud.Devices", "ReceiptFooter", c => c.String(nullable: false, maxLength: 150, unicode: false));
            AddColumn("PosCloud.Devices", "RefundPin", c => c.String(nullable: false, maxLength: 8000, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("PosCloud.Devices", "RefundPin");
            DropColumn("PosCloud.Devices", "ReceiptFooter");
            DropColumn("PosCloud.Devices", "ReceiptHeader");
            DropColumn("PosCloud.Stores", "Currency");
            DropColumn("PosCloud.Stores", "BusinessStartTime");
        }
    }
}
