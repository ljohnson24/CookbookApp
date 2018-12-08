using Cookbook.Core.Models;
using Cookbook.Core.ViewModels;
using Cookbook.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Cookbook.Controllers
{
    public class ProfileController : Controller
    {
        // GET: /
        public ActionResult Index()
        {
            return View();
        }

        // POST: Profile/LiveSearch
        [HttpPost]
        public JsonResult LiveSearch(string searchVal)
        {
            // Init db
            DataContext db = new DataContext();

            // Create list
            List<LiveSearchUserViewModel> usernames = db.Users.Where(x => x.Username.Contains(searchVal) && x.Username != User.Identity.Name).ToArray().Select(x => new LiveSearchUserViewModel(x)).ToList();

            // Return json
            return Json(usernames);
        }

        // POST: Profile/AddFriend
        [HttpPost]
        public void AddFriend(string friend)
        {
            // Init db
            DataContext db = new DataContext();

            // Get user's id
            User userDTO = db.Users.Where(x => x.Username.Equals(User.Identity.Name)).FirstOrDefault();
            string userId = userDTO.Id;

            // Get friend to be id
            User userDTO2 = db.Users.Where(x => x.Username.Equals(friend)).FirstOrDefault();
            string friendId = userDTO2.Id;

            // Add DTO

            Friend friendDTO = new Friend();

            friendDTO.User1 = userId;
            friendDTO.User2 = friendId;
            friendDTO.Active = false;

            db.Friends.Add(friendDTO);

            db.SaveChanges();
        }

        // POST: Profile/DisplayFriendRequests
        [HttpPost]
        public JsonResult DisplayFriendRequests()
        {
            // Init db
            DataContext db = new DataContext();

            // Get user id
            User userDTO = db.Users.Where(x => x.Username.Equals(User.Identity.Name)).FirstOrDefault();
            string userId = userDTO.Id;

            // Create list of fr
            List<FriendRequestViewModel> list = db.Friends.Where(x => x.User2 == userId && x.Active == false).ToArray().Select(x => new FriendRequestViewModel(x)).ToList();

            // Init list of users

            List<User> users = new List<User>();

            foreach (var item in list)
            {
                var user = db.Users.Where(x => x.Id == item.User1).FirstOrDefault();
                users.Add(user);
            }

            // Return json
            return Json(users);
        }

        // POST: Profile/AcceptFriendRequest
        [HttpPost]
        public void AcceptFriendRequest(string friendId)
        {
            // Init db
            DataContext db = new DataContext();

            // Get user id
            User userDTO = db.Users.Where(x => x.Username.Equals(User.Identity.Name)).FirstOrDefault();
            string userId = userDTO.Id;

            // Make friends

            Friend friendDTO = db.Friends.Where(x => x.User1 == friendId && x.User2 == userId).FirstOrDefault();

            friendDTO.Active = true;

            db.SaveChanges();
        }

        // POST: Profile/DeclineFriendRequest
        [HttpPost]
        public void DeclineFriendRequest(string friendId)
        {
            // Init db
            DataContext db = new DataContext();

            // Get user id
            User userDTO = db.Users.Where(x => x.Username.Equals(User.Identity.Name)).FirstOrDefault();
            string userId = userDTO.Id;

            // Delete friend request

            Friend friendDTO = db.Friends.Where(x => x.User1 == friendId && x.User2 == userId).FirstOrDefault();

            db.Friends.Remove(friendDTO);

            db.SaveChanges();
        }

        // POST: Profile/SendMessage
        [HttpPost]
        public void SendMessage(string friend, string message)
        {
            // Init db
            DataContext db = new DataContext();

            // Get user id
            User userDTO = db.Users.Where(x => x.Username.Equals(User.Identity.Name)).FirstOrDefault();
            string userId = userDTO.Id;

            // Get friend id
            User userDTO2 = db.Users.Where(x => x.Username.Equals(friend)).FirstOrDefault();
            string userId2 = userDTO2.Id;

            // Save message

            Messenger dto = new Messenger();

            dto.From = userId;
            dto.To = userId2;
            dto.Message = message;
            dto.DateSent = DateTime.Now;
            dto.Read = false;

            db.Messages.Add(dto);
            db.SaveChanges();
        }

        // POST: Profile/DisplayUnreadMessages
        [HttpPost]
        public JsonResult DisplayUnreadMessages()
        {
            // Init db
            DataContext db = new DataContext();

            // Get user id
            User userDTO = db.Users.Where(x => x.Username.Equals(User.Identity.Name)).FirstOrDefault();
            string userId = userDTO.Id;

            // Create a list of unread messages
            List<MessageViewModel> list = db.Messages.Where(x => x.To == userId && x.Read == false).ToArray().Select(x => new MessageViewModel(x)).ToList();

            // Make unread read
            db.Messages.Where(x => x.To == userId && x.Read == false).ToList().ForEach(x => x.Read = true);
            db.SaveChanges();

            // Return json
            return Json(list);
        }

        // POST: Profile/UpdateWallMessage
        [HttpPost]
        public void UpdateWallMessage(int id, string message)
        {
            // Init db
            DataContext db = new DataContext();

            // Update wall
            Wall wall = db.Wall.Find(id);

            wall.Message = message;
            wall.DateEdited = DateTime.Now;

            db.SaveChanges();
        }
    }
}