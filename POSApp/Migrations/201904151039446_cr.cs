namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cr : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.Clients", "Currency", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("PosCloud.Clients", "Currency");
        }
    }
}
