namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bbbb : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.TransDetails", "Balance", c => c.Decimal(nullable: false, precision: 18, scale: 2,defaultValueSql:"0"));
        }
        
        public override void Down()
        {
            AlterColumn("PosCloud.TransDetails", "Balance", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
