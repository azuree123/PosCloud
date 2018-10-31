namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppCounters",
                c => new
                    {
                        Id = c.Byte(nullable: false, identity: true),
                        InvoiceTransId = c.Int(nullable: false),
                        PurchaseTransId = c.Int(nullable: false),
                        Voucher = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        FiscalYearId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AppCounters");
        }
    }
}
