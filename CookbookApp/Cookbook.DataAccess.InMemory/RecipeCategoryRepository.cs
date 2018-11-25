using Cookbook.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.DataAccess.InMemory
{
    public class RecipeCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default; //initialize cache memory object
        List<RecipeCategory> recipeCategories; //declare recipe list

        //constructor
        public RecipeCategoryRepository()
        {
            //set recipes list to cache recipes representation as list
            recipeCategories = cache["recipeCategories"] as List<RecipeCategory>;

            //Check if recipes list is empty
            if (recipeCategories == null)
            {
                recipeCategories = new List<RecipeCategory>();
            }
        }

        //pushes recipes list to the cache memory
        public void Commit()
        {
            cache["recipeCategories"] = recipeCategories;
        }

        //add a recipecategory to the collection
        public void Insert(RecipeCategory recipeCategory)
        {
            recipeCategories.Add(recipeCategory);
        }

        //update a recipe category from the recipecategorys collection
        public void Update(RecipeCategory recipeCategory)
        {
            RecipeCategory recipeCategoryToUpdate = recipeCategories.Find(rc => rc.Id == recipeCategory.Id); //lambda for retrieving objects in collections

            //check if recipe to be updated exists (not null)
            if (recipeCategoryToUpdate != null)
            {
                recipeCategoryToUpdate = recipeCategory;
            }
            else
            {
                throw new Exception("RecipeCategory not found");
            }
        }

        //fetches a recipe by id
        public RecipeCategory Find(string Id)
        {
            RecipeCategory recipeCategory = recipeCategories.Find(rc => rc.Id == Id); //lambda for retrieving objects in collections

            //check if recipe to be updated exists (not null)
            if (recipeCategory != null)
            {
                return recipeCategory;
            }
            else
            {
                throw new Exception("RecipeCategory not found");
            }
        }

        //returns the list as a queryable list
        public IQueryable<RecipeCategory> Collection()
        {
            return recipeCategories.AsQueryable();
        }

        //deletes recipe
        public void Delete(string Id)
        {
            RecipeCategory recipeCategoryToDelete = recipeCategories.Find(rc => rc.Id == Id); //lambda for retrieving objects in collections

            //check if recipe to be deleted exists (not null)
            if (recipeCategoryToDelete != null)
            {
                recipeCategories.Remove(recipeCategoryToDelete);//deletes recipe
            }
            else
            {
                throw new Exception("RecipeCategory not found");
            }
        }
    }
}
