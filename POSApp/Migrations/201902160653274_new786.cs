namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new786 : DbMigration
    {
        public override void Up()
        {
            AddColumn("PosCloud.Stores", "ClientId", c => c.Int());
            AddColumn("PosCloud.TransMaster", "Issued", c => c.Boolean());
            AlterColumn("PosCloud.Clients", "Name", c => c.String(nullable: false, maxLength: 150, unicode: false));
            AlterColumn("PosCloud.Clients", "Address", c => c.String(nullable: false, maxLength: 150, unicode: false));
            AlterColumn("PosCloud.Clients", "Contact", c => c.String(nullable: false, maxLength: 150, unicode: false));
            AlterColumn("PosCloud.Clients", "City", c => c.String(maxLength: 150, unicode: false));
            AlterColumn("PosCloud.Clients", "State", c => c.String(maxLength: 150, unicode: false));
            CreateIndex("PosCloud.Stores", "ClientId");
            AddForeignKey("PosCloud.Stores", "ClientId", "PosCloud.Clients", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("PosCloud.Stores", "ClientId", "PosCloud.Clients");
            DropIndex("PosCloud.Stores", new[] { "ClientId" });
            AlterColumn("PosCloud.Clients", "State", c => c.String(maxLength: 150));
            AlterColumn("PosCloud.Clients", "City", c => c.String(maxLength: 150));
            AlterColumn("PosCloud.Clients", "Contact", c => c.String(maxLength: 150));
            AlterColumn("PosCloud.Clients", "Address", c => c.String(maxLength: 300));
            AlterColumn("PosCloud.Clients", "Name", c => c.String());
            DropColumn("PosCloud.TransMaster", "Issued");
            DropColumn("PosCloud.Stores", "ClientId");
        }
    }
}
