namespace Cookbook.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RecipeCategories");
            DropTable("dbo.Recipes");
            DropTable("dbo.IngredientTypes");
        }
    }
}
