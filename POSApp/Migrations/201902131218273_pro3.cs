namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pro3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("PosCloud.Products", "CostPrice", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("PosCloud.Products", "CostPrice", c => c.Double(nullable: false));
        }
    }
}
