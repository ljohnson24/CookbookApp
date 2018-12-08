using Cookbook.Core.Contracts;
using Cookbook.Core.Models;
using Cookbook.Core.ViewModels;
using Cookbook.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Cookbook.Controllers
{
    public class AccountController : Controller
    {

        IRepository<User> userContext;
        IRepository<Wall> wallContext;
        IRepository<Friend> friendContext;
        IRepository<Messenger> messageContext;

        public AccountController() { }
        public AccountController(IRepository<User> usercontext, IRepository<Wall> wallcontext, IRepository<Friend> friendcontext, IRepository<Messenger> messagecontext)
        {
            userContext = usercontext;
            wallContext = wallcontext;
            friendContext = friendcontext;
            messageContext = messagecontext;
            
        }
        // GET: /
        public ActionResult Index()
        {
            // Confirm user is not logged in

            string username = User.Identity.Name;

            if (!string.IsNullOrEmpty(username))
                return Redirect("~/" + username);

            // Return view
            return View();
        }

        // POST: Account/CreateAccount
        [HttpPost]
        public ActionResult CreateAccount(UserViewModel model, HttpPostedFileBase file)
        {
            // Init db
            //Db db = new Db();

            // Check model state
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            // Make sure username is unique
            if (userContext.Find(model.Username) != null)
            {
                ModelState.AddModelError("", "Username " + model.Username + " is taken.");
                model.Username = "";
                return View("Index", model);
            }

            // Create UserDTO
            User userDTO = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailAddress = model.EmailAddress,
                Username = model.Username,
                Password = model.Password
            };

            // Add to DTO
            userContext.Insert(userDTO);

            // Save
            userContext.Commit();

            // Get inserted id
            string userId = userDTO.Id;

            // Login user
            FormsAuthentication.SetAuthCookie(model.Username, false);

            // Set uploads dir
            var uploadsDir = new DirectoryInfo(string.Format("{0}Uploads", Server.MapPath(@"\")));

            // Check if a file was uploaded
            if (file != null && file.ContentLength > 0)
            {
                // Get extension
                string ext = file.ContentType.ToLower();

                // Verify extension
                if (ext != "image/jpg" &&
                    ext != "image/jpeg" &&
                    ext != "image/pjpeg" &&
                    ext != "image/gif" &&
                    ext != "image/x-png" &&
                    ext != "image/png")
                {
                    ModelState.AddModelError("", "The image was not uploaded - wrong image extension.");
                    return View("Index", model);
                }

                // Set image name
                string imageName = userId + ".jpg";

                // Set image path
                var path = string.Format("{0}\\{1}", uploadsDir, imageName);

                // Save image
                file.SaveAs(path);
            }

            // Add to wall

            Wall wall = new Wall();

            wall.Id = userId;
            wall.Message = "";
            wall.DateEdited = DateTime.Now;

            wallContext.Insert(wall);
            wallContext.Commit();

            // Redirect
            return Redirect("~/" + model.Username);
        }

        // GET: /{username}
        [Authorize]
        public ActionResult Username(string username = "")
        {
            // Init db
            //Db db = new Db();

            // Check if user exists
            if (!userContext.Collection().Any(x => x.Username.Equals(username)))
            {
                return Redirect("~/");
            }

            // ViewBag username
            ViewBag.Username = username;

            // Get logged in user's username
            string user = User.Identity.Name;

            // Viewbag user's full name
            User userDTO = userContext.Collection().Where(x => x.Username.Equals(user)).FirstOrDefault();
            ViewBag.FullName = userDTO.FirstName + " " + userDTO.LastName;

            // Get user's id
            string userId = userDTO.Id;

            // ViewBag user id
            ViewBag.UserId = userId;

            // Get viewing full name
            User userDTO2 = userContext.Collection().Where(x => x.Username.Equals(username)).FirstOrDefault();
            ViewBag.ViewingFullName = userDTO2.FirstName + " " + userDTO2.LastName;

            // Get username's image
            ViewBag.UsernameImage = userDTO2.Id + ".jpg";

            // Viewbag user type

            string userType = "guest";

            if (username.Equals(user))
                userType = "owner";

            ViewBag.UserType = userType;

            // Check if they are friends

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

            var friendCount2 = friendContext.Collection().Friends.Count(x => x.User2 == usernameId && x.Active == true || x.User1 == usernameId && x.Active == true);

            ViewBag.FCount = friendCount2;

            // Viewbag message count

            var messageCount = .Messages.Count(x => x.To == userId && x.Read == false);

            ViewBag.MsgCount = messageCount;

            // Viewbag user wall
            Wall wall = new Wall();
            ViewBag.WallMessage = db.Wall.Where(x => x.Id == userId).Select(x => x.Message).FirstOrDefault();

            // Viewbag friend walls

            List<string> friendIds1 = db.Friends.Where(x => x.User1 == userId && x.Active == true).ToArray().Select(x => x.User2).ToList();

            List<string> friendIds2 = db.Friends.Where(x => x.User2 == userId && x.Active == true).ToArray().Select(x => x.User1).ToList();

            List<string> allFriendsIds = friendIds1.Concat(friendIds2).ToList();

            List<WallViewModel> walls = db.Wall.Where(x => allFriendsIds.Contains(x.Id)).ToArray().OrderByDescending(x => x.DateEdited).Select(x => new WallViewModel(x)).ToList();

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
            // Init db
            //Db db = new Db();

            // Check if user exists
            if (db.Users.Any(x => x.Username.Equals(username) && x.Password.Equals(password)))
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