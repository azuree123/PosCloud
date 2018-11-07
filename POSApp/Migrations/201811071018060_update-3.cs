namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("PosCloud.Discounts", "ValidFrom", c => c.DateTime(nullable: false));
            AlterColumn("PosCloud.Discounts", "ValidTill", c => c.DateTime(nullable: false));
            AlterColumn("PosCloud.Discounts", "IsPercentage", c => c.Boolean());
            AlterColumn("PosCloud.Discounts", "IsTaxable", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("PosCloud.Discounts", "IsTaxable", c => c.Boolean(nullable: false));
            AlterColumn("PosCloud.Discounts", "IsPercentage", c => c.Boolean(nullable: false));
            AlterColumn("PosCloud.Discounts", "ValidTill", c => c.DateTime());
            AlterColumn("PosCloud.Discounts", "ValidFrom", c => c.DateTime());
        }
    }
}
