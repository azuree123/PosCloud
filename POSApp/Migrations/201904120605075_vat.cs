namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vat : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.Clients", "VatNumber", c => c.String(maxLength: 150, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("PosCloud.Clients", "VatNumber");
        }
    }
}
