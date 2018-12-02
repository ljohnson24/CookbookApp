using Cookbook.Core.Contracts;
using Cookbook.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cookbook.WebUI.Controllers
{
    public class IngredientTypeController : Controller
    {
        IRepository<IngredientType> context;//for inmemory transactions

        //constructor
        public IngredientTypeController(IRepository<IngredientType> context)
        {
            this.context = context;//initializes inmemory context
        }

        // GET: IngredienttypeManager
        public ActionResult Index()
        {
            List<IngredientType> ingredientTypes = context.Collection().ToList(); //sets context to list
            return View(ingredientTypes);//passes recipes to view
        }

        public ActionResult Create()
        {
            IngredientType ingredientType = new IngredientType();
            return View(ingredientType);
        }

        //creates end-point
        [HttpPost]
        public ActionResult Create(IngredientType ingredientType)
        {
            //check if data validation passed
            if (!ModelState.IsValid)
            {
                return View(ingredientType);//returns if data validation is unsuccessful
            }
            else
            {
                context.Insert(ingredientType); //adds the recipe
                context.Commit(); //saves changes to cache memory

                return RedirectToAction("Index");//redirects to index page
            }
        }

        public ActionResult Edit(string Id)
        {
            IngredientType ingredientTypeToEdit = context.Find(Id);//search(data) for recipe by id

            if (ingredientTypeToEdit == null) //if not found
            {
                return HttpNotFound();//issue an http error
            }
            else
            {
                return View(ingredientTypeToEdit);//return recipe
            }
        }

        [HttpPost]
        public ActionResult Edit(IngredientType ingredientType, string Id)
        {
            IngredientType ingredientTypeToEdit = context.Find(Id);

            if (ingredientTypeToEdit == null) //if not found
            {
                return HttpNotFound();//issue an http error
            }
            else
            {
                if (!ModelState.IsValid) //data validation test
                {
                    return View(ingredientTypeToEdit);//reload page
                }

                ingredientTypeToEdit.Type = ingredientType.Type;

                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            IngredientType ingredientTypeToDelete = context.Find(Id);

            if (ingredientTypeToDelete == null) //if not found
            {
                return HttpNotFound();//issue an http error
            }
            else
            {
                return View(ingredientTypeToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            IngredientType ingredientTypeToDelete = context.Find(Id);

            if (ingredientTypeToDelete == null) //if not found
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