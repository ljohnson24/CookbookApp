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
        
    }
}
