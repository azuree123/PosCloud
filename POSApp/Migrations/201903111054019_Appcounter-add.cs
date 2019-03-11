namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Appcounteradd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppCounters", "HoldInvoiceTransId", c => c.Int(nullable: false,defaultValueSql:"100"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppCounters", "HoldInvoiceTransId");
        }
    }
}
