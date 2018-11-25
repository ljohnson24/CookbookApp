using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cookbook.Core.Models;
using Cookbook.DataAccess.InMemory;

namespace Cookbook.WebUI.Controllers
{
    public class RecipeManagerController : Controller
    {
        RecipeRepository context;//for inmemory transactions

        //constructor
        public RecipeManagerController()
        {
            context = new RecipeRepository();//initializes inmemory context
        }

        // GET: RecipeManager
        public ActionResult Index()
        {
            List<Recipe> recipes = context.Collection().ToList(); //sets context to list
            return View(recipes);//passes recipes to view
        }

        public ActionResult Create()
        {
            Recipe recipe = new Recipe();
            return View(recipe);
        }

        //creates recipe
        [HttpPost]
        public ActionResult Create(Recipe recipe)
        {
            //check if data validation passed
            if (!ModelState.IsValid)
            {
                return View(recipe);//returns if data validation is unsuccessful
            }
            else
            {
                context.Insert(recipe); //adds the recipe
                context.Commit(); //saves changes to cache memory

                return RedirectToAction("Index");//redirects to index page
            }
        }

        public ActionResult Edit(string Id)
        {
            Recipe recipe = context.Find(Id);//search(data) for recipe by id
            
            if (recipe == null) //if not found
            {
                return HttpNotFound();//issue an http error
            }
            else
            {
                return View(recipe);//return recipe
            }
        }

        [HttpPost]
        public ActionResult Edit(Recipe recipe, string Id)
        {
            Recipe recipeToEdit = context.Find(Id);

            if (recipeToEdit == null) //if not found
            {
                return HttpNotFound();//issue an http error
            }
            else
            {
                if (!ModelState.IsValid) //data validation test
                {
                    return View(recipe);//reload page
                }

                recipeToEdit.Category = recipe.Category;
                recipeToEdit.Description = recipe.Description;
                recipeToEdit.Image = recipe.Image;
                recipeToEdit.Name = recipe.Name;
                recipeToEdit.Price = recipe.Price;

                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Recipe recipeToDelete = context.Find(Id);

            if (recipeToDelete == null) //if not found
            {
                return HttpNotFound();//issue an http error
            }
            else
            {
                return View(recipeToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(Recipe recipe, string Id)
        {
            Recipe recipeToDelete = context.Find(Id);

            if (recipeToDelete == null) //if not found
            {
                return HttpNotFound();//issue an http error
            }
            else
            {
                context.Delete(Id);//deletes from database
                context.Commit();//saves changes
                return RedirectToAction("Index");
            }
        }
    }
}