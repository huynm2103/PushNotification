namespace PushNotification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        CloudId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        LoginType = c.String(),
                        UserType = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        LastLogin = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        Email = c.String(),
                        IsVerify = c.Boolean(nullable: false),
                        VerifyCode = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserInfo", "UserID", "dbo.User");
            DropIndex("dbo.UserInfo", new[] { "UserID" });
            DropTable("dbo.User");
            DropTable("dbo.UserInfo");
        }
    }
}
