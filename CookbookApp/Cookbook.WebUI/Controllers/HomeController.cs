using Cookbook.Core.Contracts;
using Cookbook.Core.Models;
using Cookbook.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cookbook.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Recipe> context;//for inmemory transactions
        IRepository<RecipeCategory> recipeCategories;
        
        //constructor
        public HomeController(IRepository<Recipe> recipeContext, IRepository<RecipeCategory> recipeCategoryContext)
        {
            context = recipeContext;//initializes inmemory context
            recipeCategories = recipeCategoryContext;
        }
        public ActionResult Index(string Category = null)
        {
            List<Recipe> recipes;
            List<RecipeCategory> categories = recipeCategories.Collection().ToList();

            if (Category == null)
            {
                recipes=context.Collection().ToList();
            }
            else
            {
                recipes = context.Collection().Where(p => p.Category == Category).ToList();
            }

            RecipeListViewModel model = new RecipeListViewModel();
            model.recipes = recipes;
            model.recipeCategories = categories;
            return View(model);
        }

        public ActionResult Details(string Id)
        {
            Recipe recipe = context.Find(Id);
            if(recipe == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(recipe);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}