namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new787 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("PosCloud.TransMaster", "Issued", c => c.Boolean(nullable: false,defaultValueSql:"0"));
        }
        
        public override void Down()
        {
            AlterColumn("PosCloud.TransMaster", "Issued", c => c.Boolean());
        }
    }
}
