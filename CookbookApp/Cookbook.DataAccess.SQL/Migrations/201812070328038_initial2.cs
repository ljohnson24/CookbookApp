namespace Cookbook.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Onlines",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ConnId = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        Users_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Users_Id)
                .Index(t => t.Users_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Onlines", "Users_Id", "dbo.Users");
            DropIndex("dbo.Onlines", new[] { "Users_Id" });
            DropTable("dbo.Onlines");
        }
    }
}
