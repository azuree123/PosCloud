namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class csitem : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.ModifierTransDetails", "ComboSubItem", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("PosCloud.ModifierTransDetails", "ComboSubItem");
        }
    }
}
