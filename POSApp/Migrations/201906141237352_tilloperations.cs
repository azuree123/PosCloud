namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tilloperations : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.TillOperations", "CarryOut", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("PosCloud.TillOperations", "AdjustedCashAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("PosCloud.TillOperations", "AdjustedCreditAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("PosCloud.TillOperations", "AdjustedCreditNoteAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("PosCloud.TillOperations", "AdjustedCreditNoteAmount");
            DropColumn("PosCloud.TillOperations", "AdjustedCreditAmount");
            DropColumn("PosCloud.TillOperations", "AdjustedCashAmount");
            DropColumn("PosCloud.TillOperations", "CarryOut");
        }
    }
}
