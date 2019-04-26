namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class psw : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.Clients", "Password", c => c.String());
            AddColumn("PosCloud.Clients", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("PosCloud.Clients", "Email");
            DropColumn("PosCloud.Clients", "Password");
        }
    }
}
