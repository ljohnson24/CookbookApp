namespace Cookbook.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        User1 = c.String(),
                        User2 = c.String(),
                        Active = c.Boolean(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        Users1_Id = c.String(maxLength: 128),
                        Users2_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Users1_Id)
                .ForeignKey("dbo.Users", t => t.Users2_Id)
                .Index(t => t.Users1_Id)
                .Index(t => t.Users2_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LstName = c.String(),
                        EmailAddress = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messengers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        From = c.String(),
                        To = c.String(),
                        Message = c.String(),
                        DateSent = c.DateTime(nullable: false),
                        Read = c.Boolean(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        FromUsers_Id = c.String(maxLength: 128),
                        ToUsers_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.FromUsers_Id)
                .ForeignKey("dbo.Users", t => t.ToUsers_Id)
                .Index(t => t.FromUsers_Id)
                .Index(t => t.ToUsers_Id);
            
            CreateTable(
                "dbo.Walls",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Message = c.String(),
                        DateEdited = c.DateTime(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        Users_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Users_Id)
                .Index(t => t.Users_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Walls", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.Messengers", "ToUsers_Id", "dbo.Users");
            DropForeignKey("dbo.Messengers", "FromUsers_Id", "dbo.Users");
            DropForeignKey("dbo.Friends", "Users2_Id", "dbo.Users");
            DropForeignKey("dbo.Friends", "Users1_Id", "dbo.Users");
            DropIndex("dbo.Walls", new[] { "Users_Id" });
            DropIndex("dbo.Messengers", new[] { "ToUsers_Id" });
            DropIndex("dbo.Messengers", new[] { "FromUsers_Id" });
            DropIndex("dbo.Friends", new[] { "Users2_Id" });
            DropIndex("dbo.Friends", new[] { "Users1_Id" });
            DropTable("dbo.Walls");
            DropTable("dbo.Messengers");
            DropTable("dbo.Users");
            DropTable("dbo.Friends");
        }
    }
}
