using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using Cookbook.Core.Models;

namespace Cookbook.DataAccess.InMemory
{
    public class RecipeRepository
    {
        ObjectCache cache = MemoryCache.Default; //initialize cache memory object
        List<Recipe> recipes; //declare recipe list

        //constructor
        public RecipeRepository()
        {
            //set recipes list to cache recipes representation as list
            recipes = cache["recipes"] as List<Recipe>;

            //Check if recipes list is empty
            if (recipes == null)
            {
                recipes = new List<Recipe>();
            }
        }

        //pushes recipes list to the cache memory
        public void Commit()
        {
            cache["recipes"] = recipes;
        }

        //add a recipe to the recipes collection
        public void Insert(Recipe recipe)
        {
            recipes.Add(recipe);
        }

        //update a recipe from the recipes collection
        public void Update(Recipe recipe)
        {
            Recipe recipeToUpdate = recipes.Find(r => r.Id == recipe.Id); //lambda for retrieving objects in collections

            //check if recipe to be updated exists (not null)
            if (recipeToUpdate != null)
            {
                recipeToUpdate = recipe;
            }
            else
            {
                throw new Exception("Recipe not found");
            }            
        }

        //fetches a recipe by id
        public Recipe Find(string Id)
        {
            Recipe recipe = recipes.Find(r => r.Id == Id); //lambda for retrieving objects in collections

            //check if recipe to be updated exists (not null)
            if (recipe != null)
            {
                return recipe;
            }
            else
            {
                throw new Exception("Recipe not found");
            }
        }

        //returns the list as a queryable list
        public IQueryable<Recipe> Collection()
        {
            return recipes.AsQueryable();
        }

        //deletes recipe
        public void Delete(string Id)
        {
            Recipe recipeToDelete = recipes.Find(r => r.Id == Id); //lambda for retrieving objects in collections

            //check if recipe to be deleted exists (not null)
            if (recipeToDelete != null)
            {
                recipes.Remove(recipeToDelete);//deletes recipe
            }
            else
            {
                throw new Exception("Recipe not found");
            }
        }
    }
}
