namespace PushNotification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        CreatorID = c.Int(nullable: false),
                        Content = c.String(),
                        SentTime = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        IsLoop = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.User", t => t.CreatorID)
                .Index(t => t.CreatorID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Message", "CreatorID", "dbo.User");
            DropForeignKey("dbo.UserInfo", "UserID", "dbo.User");
            DropIndex("dbo.UserInfo", new[] { "UserID" });
            DropIndex("dbo.Message", new[] { "CreatorID" });
            DropTable("dbo.UserInfo");
            DropTable("dbo.User");
            DropTable("dbo.Message");
        }
    }
}
