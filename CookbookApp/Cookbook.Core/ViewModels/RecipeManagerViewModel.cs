using Cookbook.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Core.ViewModels
{
    public class RecipeManagerViewModel
    {
        public Recipe recipe { set; get; }
        public IEnumerable<RecipeCategory> recipeCategories { get; set; }
        public IEnumerable<IngredientType> ingredientTypes { get; set; }

    }
}
