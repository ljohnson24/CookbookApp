using Cookbook.Core;
using Cookbook.Core.Contracts;
using Cookbook.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cookbook.WebUI.Controllers
{
    public class RecipeCategoryManagerController : Controller
    {
        IRepository<RecipeCategory> context;//for inmemory transactions
        
        //constructor
        public RecipeCategoryManagerController(IRepository<RecipeCategory> context)
        {
            this.context = context;//initializes inmemory context
        }

        // GET: RecipeCategoryManager
        public ActionResult Index()
        {
            List<RecipeCategory> recipeCategories = context.Collection().ToList(); //sets context to list
            return View(recipeCategories);//passes recipes to view
        }

        public ActionResult Create()
        {
            RecipeCategory recipeCategory = new RecipeCategory();
            return View(recipeCategory);
        }

        //creates recipeCategory
        [HttpPost]
        public ActionResult Create(RecipeCategory recipeCategory)
        {
            //check if data validation passed
            if (!ModelState.IsValid)
            {
                return View(recipeCategory);//returns if data validation is unsuccessful
            }
            else
            {
                context.Insert(recipeCategory); //adds the recipe
                context.Commit(); //saves changes to cache memory

                return RedirectToAction("Index");//redirects to index page
            }
        }

        public ActionResult Edit(string Id)
        {
            RecipeCategory recipeCategory = context.Find(Id);//search(data) for recipe by id

            if (recipeCategory == null) //if not found
            {
                return HttpNotFound();//issue an http error
            }
            else
            {
                return View(recipeCategory);//return recipe
            }
        }

        [HttpPost]
        public ActionResult Edit(RecipeCategory recipeCategory, string Id)
        {
            RecipeCategory recipeCategoryToEdit = context.Find(Id);

            if (recipeCategoryToEdit == null) //if not found
            {
                return HttpNotFound();//issue an http error
            }
            else
            {
                if (!ModelState.IsValid) //data validation test
                {
                    return View(recipeCategory);//reload page
                }

                recipeCategoryToEdit.Category = recipeCategory.Category;
                
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            RecipeCategory recipeCategoryToDelete = context.Find(Id);

            if (recipeCategoryToDelete == null) //if not found
            {
                return HttpNotFound();//issue an http error
            }
            else
            {
                return View(recipeCategoryToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            RecipeCategory recipeCategoryToDelete = context.Find(Id);

            if (recipeCategoryToDelete == null) //if not found
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