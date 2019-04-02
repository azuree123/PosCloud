namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trs : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.TransMaster", "TransferTo", c => c.String(maxLength: 25, unicode: false));
            DropColumn("PosCloud.TransMaster", "ToStoreId");
        }
        
        public override void Down()
        {
            AddColumn("PosCloud.TransMaster", "ToStoreId", c => c.Int());
            DropColumn("PosCloud.TransMaster", "TransferTo");
        }
    }
}
