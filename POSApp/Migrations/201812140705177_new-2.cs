namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.TransMaster", "DeliveryType", c => c.String(maxLength: 25, unicode: false));
            AddColumn("PosCloud.TransMaster", "Address", c => c.String(maxLength: 150, unicode: false));
            AddColumn("PosCloud.TransMaster", "ContactNumber", c => c.String(maxLength: 25, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("PosCloud.TransMaster", "ContactNumber");
            DropColumn("PosCloud.TransMaster", "Address");
            DropColumn("PosCloud.TransMaster", "DeliveryType");
        }
    }
}
