namespace Cookbook.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        User1 = c.String(maxLength: 128),
                        User2 = c.String(maxLength: 128),
                        Active = c.Boolean(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User1)
                .ForeignKey("dbo.Users", t => t.User2)
                .Index(t => t.User1)
                .Index(t => t.User2);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmailAddress = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IngredientTypes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Type = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messengers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        From = c.String(maxLength: 128),
                        To = c.String(maxLength: 128),
                        Message = c.String(),
                        DateSent = c.DateTime(nullable: false),
                        Read = c.Boolean(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.From)
                .ForeignKey("dbo.Users", t => t.To)
                .Index(t => t.From)
                .Index(t => t.To);
            
            CreateTable(
                "dbo.Onlines",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ConnId = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 20),
                        Description = c.String(),
                        Category = c.String(),
                        Image = c.String(),
                        ServingSize = c.Double(nullable: false),
                        Calories = c.Int(nullable: false),
                        ingredient1 = c.String(),
                        ingredient2 = c.String(),
                        ingredient3 = c.String(),
                        ingredient4 = c.String(),
                        ingredient5 = c.String(),
                        ingredient6 = c.String(),
                        ingredient7 = c.String(),
                        ingredient8 = c.String(),
                        ingredient9 = c.String(),
                        ingredient10 = c.String(),
                        instruction1 = c.String(),
                        instruction2 = c.String(),
                        instruction3 = c.String(),
                        instruction4 = c.String(),
                        instruction5 = c.String(),
                        instruction6 = c.String(),
                        instruction7 = c.String(),
                        instruction8 = c.String(),
                        instruction9 = c.String(),
                        instruction10 = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RecipeCategories",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Category = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Walls",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Message = c.String(),
                        DateEdited = c.DateTime(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Walls", "Id", "dbo.Users");
            DropForeignKey("dbo.Onlines", "Id", "dbo.Users");
            DropForeignKey("dbo.Messengers", "To", "dbo.Users");
            DropForeignKey("dbo.Messengers", "From", "dbo.Users");
            DropForeignKey("dbo.Friends", "User2", "dbo.Users");
            DropForeignKey("dbo.Friends", "User1", "dbo.Users");
            DropIndex("dbo.Walls", new[] { "Id" });
            DropIndex("dbo.Onlines", new[] { "Id" });
            DropIndex("dbo.Messengers", new[] { "To" });
            DropIndex("dbo.Messengers", new[] { "From" });
            DropIndex("dbo.Friends", new[] { "User2" });
            DropIndex("dbo.Friends", new[] { "User1" });
            DropTable("dbo.Walls");
            DropTable("dbo.RecipeCategories");
            DropTable("dbo.Recipes");
            DropTable("dbo.Onlines");
            DropTable("dbo.Messengers");
            DropTable("dbo.IngredientTypes");
            DropTable("dbo.Users");
            DropTable("dbo.Friends");
        }
    }
}
