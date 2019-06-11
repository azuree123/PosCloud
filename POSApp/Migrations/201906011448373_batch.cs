namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class batch : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.TransDetails", "BatchNumber", c => c.String(maxLength: 4000));
            AddColumn("PosCloud.TransDetails", "ExpiryDate", c => c.DateTime());
            AddColumn("PosCloud.TransDetails", "ManufactureDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("PosCloud.TransDetails", "ManufactureDate");
            DropColumn("PosCloud.TransDetails", "ExpiryDate");
            DropColumn("PosCloud.TransDetails", "BatchNumber");
        }
    }
}
