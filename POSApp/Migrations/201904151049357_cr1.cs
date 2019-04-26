namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cr1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("PosCloud.Clients", "Currency");
        }
        
        public override void Down()
        {
            AddColumn("PosCloud.Clients", "Currency", c => c.String(maxLength: 150));
        }
    }
}
