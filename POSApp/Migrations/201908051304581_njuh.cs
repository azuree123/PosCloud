namespace POSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class njuh : DbMigration
    {
        public override void Up()
        {
            DropTable("PosCloud.Notifications");
        }
        
        public override void Down()
        {
            CreateTable(
                "PosCloud.Notifications",
                c => new
                    {
                        NotificationId = c.Int(nullable: false, identity: true),
                        NotificationMessage = c.String(maxLength: 500, unicode: false),
                        EmptyNotification = c.String(maxLength: 150, unicode: false),
                        NotificationDate = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.NotificationId);
            
        }
    }
}
