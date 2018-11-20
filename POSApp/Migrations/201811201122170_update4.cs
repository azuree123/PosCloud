namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.TimedEvents", "DiscountCode", c => c.String(nullable: false, maxLength: 7, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("PosCloud.TimedEvents", "DiscountCode");
        }
    }
}
