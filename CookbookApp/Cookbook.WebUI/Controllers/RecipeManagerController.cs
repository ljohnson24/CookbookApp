using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cookbook.Core;
using Cookbook.Core.Contracts;
using Cookbook.Core.Models;
using Cookbook.Core.ViewModels;
using Cookbook.DataAccess.InMemory;

namespace Cookbook.WebUI.Controllers
{
    public class RecipeManagerController : Controller
    {
        IRepository<Recipe> context;//for inmemory transactions
        IRepository<RecipeCategory> recipeCategories;

        //constructor
        public RecipeManagerController(IRepository<Recipe> recipeContext, IRepository<RecipeCategory> recipeCategoryContext)
        {
            context = recipeContext;//initializes inmemory context
            recipeCategories = recipeCategoryContext;
        }

        // GET: RecipeManager
        public ActionResult Index()
        {
            List<Recipe> recipes = context.Collection().ToList(); //sets context to list
            return View(recipes);//passes recipes to view
        }

        public ActionResult Create()
        {
            RecipeManagerViewModel viewModel = new RecipeManagerViewModel();
            viewModel.recipeCategories = recipeCategories.Collection();
            return View(viewModel);
        }

        //creates recipe
        [HttpPost]
        public ActionResult Create(Recipe recipe, HttpPostedFileBase file)
        {
            //check if data validation passed
            if (!ModelState.IsValid)
            {
                return View(recipe);//returns if data validation is unsuccessful
            }
            else
            {
                if(file != null)//check if file exist
                {
                    recipe.Image = recipe.Id + Path.GetExtension(file.FileName);//sets recipe image to prefix(recipeid) plus file name ext
                    file.SaveAs(Server.MapPath("//Content/RecipeImages//")+recipe.Image);//saves the photo to the disk
                }
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
                RecipeManagerViewModel viewModel = new RecipeManagerViewModel();
                viewModel.recipe = recipe;
                viewModel.recipeCategories = recipeCategories.Collection();
                return View(viewModel);//return recipe
            }
        }

        [HttpPost]
        public ActionResult Edit(Recipe recipe, string Id, HttpPostedFileBase file)
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

                if (file != null)//check if file exist
                {
                    recipeToEdit.Image = recipe.Id + Path.GetExtension(file.FileName);//sets recipe image to prefix(recipeid) plus file name ext
                    file.SaveAs(Server.MapPath("//Content/RecipeImages//") + recipeToEdit.Image);//saves the photo to the disk
                }
                recipeToEdit.Category = recipe.Category;
                recipeToEdit.Description = recipe.Description;
                recipeToEdit.Name = recipe.Name;
                //recipeToEdit.Price = recipe.Price;

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