using Cookbook.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cookbook.WebUI.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            //confirm user is not logged in
            string username = User.Identity.Name;

            if (!string.IsNullOrEmpty(username))
            {
                return Redirect("~" + username);
            }
            
            return View();
        }
        //post:account/creataccount
        //[HttpPost]
        //public ActionResult CreatAccount(UserViewModel model, HttpPostedFileBase file)
        //{
        //    return View();
        //}
    }
}