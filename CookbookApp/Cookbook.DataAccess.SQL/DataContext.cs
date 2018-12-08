using Cookbook.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.DataAccess.SQL
{
    public class DataContext:DbContext
    {
        public DataContext()
            : base("DefaultConnection") {}

        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<RecipeCategory> RecipeCategories { get; set; }
        public DbSet<IngredientType> IngredientTypes { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Messenger> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wall> Wall { get; set; }
        public DbSet<Online> Online { get; set; }
        
    }
}
