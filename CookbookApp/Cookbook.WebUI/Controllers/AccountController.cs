using Cookbook.Core.Contracts;
using Cookbook.Core.Models;
using Cookbook.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Cookbook.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IRepository<User> userContext;
        IRepository<Friend> friendContext;
        IRepository<Messenger> messageContext;
        //IRepository<Online> onlineContext;
        IRepository<Wall> wallContext;

        //constructor
        public AccountController(IRepository<User> user, IRepository<Friend> friend,
            IRepository<Messenger>message, IRepository<Wall> wall)
        {
            userContext = user;
            friendContext = friend;
            messageContext = message;
            //onlineContext = online;
            wallContext = wall;
        }
        // GET: Account
        public ActionResult Index()
        {
            //confirm user is not logged in
            string username = User.Identity.Name;

            if (!string.IsNullOrEmpty(username))
            {
                return Redirect("~/" + username);
            }
            
            return View();
        }
        public ActionResult CreateAccount()
        {
            UserViewModel model = new UserViewModel();
            
            return View(model);
        }
        //post:account/creataccount
        [HttpPost]
        public ActionResult CreatAccount(User user,Wall wall, HttpPostedFileBase file)
        {
            //check model state
            if (!ModelState.IsValid)
            {
                return View("Index", user);
            }

            //condition for unique username
            if (userContext.Collection().Any(x => x.Username.Equals(user.Username)))
            {
                ModelState.AddModelError("", "Username " + user.Username + " is taken.");
                user.Username = "";
                return View("Index", user);
            }

            userContext.Insert(user);//adds user
            userContext.Commit();

            //login user
            FormsAuthentication.SetAuthCookie(user.Username, false);

            //setup directory for uploads
            var uploadsDir = new DirectoryInfo(string.Format("{0}Uploads", Server.MapPath(@"\")));

            // verify file uploaded
            if(file != null && file.ContentLength > 0)
            {
                //get extention
                string ext = file.ContentType.ToLower();

                //Verify extentions
                if(ext != "image/jpg" &&
                   ext != "image/jpeg" &&
                   ext != "image/pjpeg" &&
                   ext != "image/gif" &&
                   ext != "image/pnp")
                {
                    ModelState.AddModelError("", "The image was not uploaded - the file type is not supported");
                    return View("Index", user);
                }
                string userId = user.Id;

                //set image name
                string imageName = userId + ".jpg";

                //set image path
                var path = string.Format("{0}\\{1}", uploadsDir, imageName);
                //save the photo
                file.SaveAs(path);
            }

            //add to wall
            wallContext.Insert(wall);
            wallContext.Commit();

            return Redirect("~/" + user.Username);
           
        }

        //Get:/{username}
        [Authorize]
        public ActionResult Username(string username = "")
        {
            //check if the user exist
            if (!userContext.Collection().Any(x => x.Username.Equals(username)))
            {
                return Redirect("~/");
            }

            //viewbag username
            ViewBag.Username = username;

            //get logged in user's name
            string user = User.Identity.Name;

            //viewbag user's fullname
            User user1 = userContext.Collection().Where(x => x.Username.Equals(user)).FirstOrDefault();
            ViewBag.FullName = user1.FirstName +" "+user1.LastName;

            //get user's id
            string userId = user1.Id;

            //viewbag user id
            ViewBag.Userid = userId;

            //get viewing full name
            User user2 = userContext.Collection().Where(x => x.Username.Equals(username)).FirstOrDefault();
            ViewBag.viewingFullName = user2.FirstName + " " + user2.LastName;

            //get usernames image
            ViewBag.usernameImage = user2.Id + ".jpg";

            //ViewBag user type
            string userType = "guest";
            if (username.Equals(user))
            {
                userType = "owner";
            }
            ViewBag.UserType = userType;
            //check if users are friends
            if (userType == "guest")
            {
                User u1 = userContext.Collection().Where(x => x.Username.Equals(user)).FirstOrDefault();
                string id1 = u1.Id;

                User u2 = userContext.Collection().Where(x => x.Username.Equals(username)).FirstOrDefault();
                string id2 = u2.Id;

                Friend f1 = friendContext.Collection().Where(x => x.User1 == id1 && x.User2 == id2).FirstOrDefault();
                Friend f2 = friendContext.Collection().Where(x => x.User2 == id1 && x.User1 == id2).FirstOrDefault();

                if (f1 == null && f2 == null)
                {
                    ViewBag.NotFriends = "True";
                }

                if (f1 != null)
                {
                    if (!f1.Active)
                    {
                        ViewBag.NotFriends = "Pending";
                    }
                }

                if (f2 != null)
                {
                    if (!f2.Active)
                    {
                        ViewBag.NotFriends = "Pending";
                    }
                }

            }

            // Viewbag request count

            var friendCount = friendContext.Collection().Count(x => x.User2 == userId && x.Active == false);

            if (friendCount > 0)
            {
                ViewBag.FRCount = friendCount;
            }

            // Viewbag friend count

            User uDTO = userContext.Collection().Where(x => x.Username.Equals(username)).FirstOrDefault();
            string usernameId = uDTO.Id;

            var friendCount2 = friendContext.Collection().Count(x => x.User2 == usernameId && x.Active == true || x.User1 == usernameId && x.Active == true);

            ViewBag.FCount = friendCount2;

            // Viewbag message count

            var messageCount = messageContext.Collection().Count(x => x.To == userId && x.Read == false);

            ViewBag.MsgCount = messageCount;

            // Viewbag user wall
            Wall wall = new Wall();
            ViewBag.WallMessage = wallContext.Collection().Where(x => x.Id == userId).Select(x => x.Message).FirstOrDefault();

            // Viewbag friend walls

            List<string> friendIds1 = friendContext.Collection().Where(x => x.User1 == userId && x.Active == true).ToArray().Select(x => x.User2).ToList();

            List<string> friendIds2 = friendContext.Collection().Where(x => x.User2 == userId && x.Active == true).ToArray().Select(x => x.User1).ToList();

            List<String> allFriendsIds = friendIds1.Concat(friendIds2).ToList();

            List<Wall> walls = wallContext.Collection().Where(x => allFriendsIds.Contains(x.Id)).ToArray().OrderByDescending(x => x.DateEdited).Select(x => new Wall()).ToList();

            ViewBag.Walls = walls;

            // Return
            return View();

        }

        // GET: account/Logout
        [Authorize]
        public ActionResult Logout()
        {
            // Sign out
            FormsAuthentication.SignOut();

            // Redirect
            return Redirect("~/");
        }

        public ActionResult LoginPartial()
        {
            return PartialView();
        }

        // POST: account/Login
        [HttpPost]
        public string Login(string username, string password)
        {

            // Check if user exists
            if (userContext.Collection().Any(x => x.Username.Equals(username) && x.Password.Equals(password)))
            {
                // Log in
                FormsAuthentication.SetAuthCookie(username, false);
                return "ok";
            }
            else
            {
                return "problem";
            }

        }
    }
}