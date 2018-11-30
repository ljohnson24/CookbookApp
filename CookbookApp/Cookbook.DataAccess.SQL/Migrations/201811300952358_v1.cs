namespace Cookbook.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
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
            
            AddColumn("dbo.Recipes", "ServingSize", c => c.Double(nullable: false));
            AddColumn("dbo.Recipes", "Calories", c => c.Int(nullable: false));
            AddColumn("dbo.Recipes", "ingredient1", c => c.String());
            AddColumn("dbo.Recipes", "ingredient2", c => c.String());
            AddColumn("dbo.Recipes", "ingredient3", c => c.String());
            AddColumn("dbo.Recipes", "ingredient4", c => c.String());
            AddColumn("dbo.Recipes", "ingredient5", c => c.String());
            AddColumn("dbo.Recipes", "ingredient6", c => c.String());
            AddColumn("dbo.Recipes", "ingredient7", c => c.String());
            AddColumn("dbo.Recipes", "ingredient8", c => c.String());
            AddColumn("dbo.Recipes", "ingredient9", c => c.String());
            AddColumn("dbo.Recipes", "ingredient10", c => c.String());
            AddColumn("dbo.Recipes", "instruction1", c => c.String());
            AddColumn("dbo.Recipes", "instruction2", c => c.String());
            AddColumn("dbo.Recipes", "instruction3", c => c.String());
            AddColumn("dbo.Recipes", "instruction4", c => c.String());
            AddColumn("dbo.Recipes", "instruction5", c => c.String());
            AddColumn("dbo.Recipes", "instruction6", c => c.String());
            AddColumn("dbo.Recipes", "instruction7", c => c.String());
            AddColumn("dbo.Recipes", "instruction8", c => c.String());
            AddColumn("dbo.Recipes", "instruction9", c => c.String());
            AddColumn("dbo.Recipes", "instruction10", c => c.String());
            AlterColumn("dbo.Recipes", "Name", c => c.String(maxLength: 35));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recipes", "Name", c => c.String(maxLength: 20));
            DropColumn("dbo.Recipes", "instruction10");
            DropColumn("dbo.Recipes", "instruction9");
            DropColumn("dbo.Recipes", "instruction8");
            DropColumn("dbo.Recipes", "instruction7");
            DropColumn("dbo.Recipes", "instruction6");
            DropColumn("dbo.Recipes", "instruction5");
            DropColumn("dbo.Recipes", "instruction4");
            DropColumn("dbo.Recipes", "instruction3");
            DropColumn("dbo.Recipes", "instruction2");
            DropColumn("dbo.Recipes", "instruction1");
            DropColumn("dbo.Recipes", "ingredient10");
            DropColumn("dbo.Recipes", "ingredient9");
            DropColumn("dbo.Recipes", "ingredient8");
            DropColumn("dbo.Recipes", "ingredient7");
            DropColumn("dbo.Recipes", "ingredient6");
            DropColumn("dbo.Recipes", "ingredient5");
            DropColumn("dbo.Recipes", "ingredient4");
            DropColumn("dbo.Recipes", "ingredient3");
            DropColumn("dbo.Recipes", "ingredient2");
            DropColumn("dbo.Recipes", "ingredient1");
            DropColumn("dbo.Recipes", "Calories");
            DropColumn("dbo.Recipes", "ServingSize");
            DropTable("dbo.IngredientTypes");
        }
    }
}
