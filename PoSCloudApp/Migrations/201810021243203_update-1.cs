namespace PoSCloudApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("PosCloud.States", "CreatedBy", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("PosCloud.States", "UpdatedBy", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("PosCloud.States", "UpdatedBy", c => c.String());
            AlterColumn("PosCloud.States", "CreatedBy", c => c.String());
        }
    }
}
