using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Cookbook.DataAccess.SQL;
using Cookbook.Core.Models;

namespace Cookbook
{
    [HubName("echo")]
    public class EchoHub : Hub
    {
        public void Hello(string message)
        {
            //Clients.All.hello();
            Trace.WriteLine(message);

            // Set clients
            var clients = Clients.All;

            // Call js function
            clients.test("this is a test");
        }

        public void Notify(string friend)
        {
            // Init db
            DataContext db = new DataContext();

            // Get friend's id
            User userDTO = db.Users.Where(x => x.Username.Equals(friend)).FirstOrDefault();
            string friendId = userDTO.Id;

            // Get fr count
            var frCount = db.Friends.Count(x => x.User2 == friendId && x.Active == false);

            // Set clients
            var clients = Clients.Others;

            // Call js function
            clients.frnotify(friend, frCount);
        }

        public void GetFrcount()
        {
            // Init db
            DataContext db = new DataContext();

            // Get user id
            User userDTO = db.Users.Where(x => x.Username.Equals(Context.User.Identity.Name)).FirstOrDefault();
            string userId = userDTO.Id;

            // Get fr count
            var friendReqCount = db.Friends.Count(x => x.User2 == userId && x.Active == false);

            // Set clients
            var clients = Clients.Caller;

            // Call js function
            clients.frcount(Context.User.Identity.Name, friendReqCount);
        }

        public void GetFcount(string friendId)
        {
            // Init db
            DataContext db = new DataContext();

            // Get user id
            User userDTO = db.Users.Where(x => x.Username.Equals(Context.User.Identity.Name)).FirstOrDefault();
            string userId = userDTO.Id;

            // Get friend count for user
            var friendCount1 = db.Friends.Count(x => x.User2 == userId && x.Active == true || x.User1 == userId && x.Active == true);

            // Get user2 username
            User userDTO2 = db.Users.Where(x => x.Id == friendId).FirstOrDefault();
            string username = userDTO2.Username;

            // Get friend count for user2
            var friendCount2 = db.Friends.Count(x => x.User2 == friendId && x.Active == true || x.User1 == friendId && x.Active == true);

            // Update chat
            UpdateChat();

            // Set clients
            var clients = Clients.All;

            // Call js function
            clients.fcount(Context.User.Identity.Name, username, friendCount1, friendCount2);

        }

        public void NotifyOfMessage(string friend)
        {
            // Init db
            DataContext db = new DataContext();

            // Get friend id
            User userDTO = db.Users.Where(x => x.Username.Equals(friend)).FirstOrDefault();
            string friendId = userDTO.Id;

            // Get message count
            var messageCount = db.Messages.Count(x => x.To == friendId && x.Read == false);

            // Set clients
            var clients = Clients.Others;

            // Call js function
            clients.msgcount(friend, messageCount);
        }

        public void NotifyOfMessageOwner()
        {
            // Init db
            DataContext db = new DataContext();

            // Get user id
            User userDTO = db.Users.Where(x => x.Username.Equals(Context.User.Identity.Name)).FirstOrDefault();
            string userId = userDTO.Id;

            // Get message count
            var messageCount = db.Messages.Count(x => x.To == userId && x.Read == false);

            // Set clients
            var clients = Clients.Caller;

            // Call js function
            clients.msgcount(Context.User.Identity.Name, messageCount);
        }

        public override Task OnConnected()
        {
            // Log user conn
            Trace.WriteLine("Here I am " + Context.ConnectionId);

            // Init db
            DataContext db = new DataContext();

            // Get user id
            User userDTO = db.Users.Where(x => x.Username.Equals(Context.User.Identity.Name)).FirstOrDefault();
            string userId = userDTO.Id;

            // Get conn id
            string connId = Context.ConnectionId;

            // Add onlineDTO

            if (! db.Online.Any(x => x.Id == userId))
            {
                Online online = new Online();

                online.Id = userId;
                online.ConnId = connId;

                db.Online.Add(online);

                db.SaveChanges();
            }

            // Get all online ids
            List<string> onlineIds = db.Online.ToArray().Select(x => x.Id).ToList();

            // Get friend ids
            List<string> friendIds1 = db.Friends.Where(x => x.User1 == userId && x.Active == true).ToArray().Select(x => x.User2).ToList();

            List<string> friendIds2 = db.Friends.Where(x => x.User2 == userId && x.Active == true).ToArray().Select(x => x.User1).ToList();

            List<string> allFriendsIds = friendIds1.Concat(friendIds2).ToList();

            // Get final set of ids
            List<string> resultList = onlineIds.Where((i) => allFriendsIds.Contains(i)).ToList();

            // Create a dict of friend ids and usernames

            Dictionary<string, string> dictFriends = new Dictionary<string, string>();

            foreach (var id in resultList)
            {
                var users = db.Users.Find(id);
                string friend = users.Username;

                if (!dictFriends.ContainsKey(id))
                {
                    dictFriends.Add(id, friend);
                }
            }

            var transformed = from key in dictFriends.Keys
                              select new { id = key, friend = dictFriends[key] };

            string json = JsonConvert.SerializeObject(transformed);

            // Set clients
            var clients = Clients.Caller;

            // Call js function
            clients.getonlinefriends(Context.User.Identity.Name, json);

            // Update chat
            UpdateChat();

            // Return
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            // Log
            Trace.WriteLine("gone - " + Context.ConnectionId + " " + Context.User.Identity.Name);

            // Init db
            DataContext db = new DataContext();

            // Get user id
            User userDTO = db.Users.Where(x => x.Username.Equals(Context.User.Identity.Name)).FirstOrDefault();
            string userId = userDTO.Id;

            // Remove from db
            if (db.Online.Any(x => x.Id == userId))
            {
                Online online = db.Online.Find(userId);
                db.Online.Remove(online);
                db.SaveChanges();
            }

            // Update chat
            UpdateChat();

            // Return
            return base.OnDisconnected(stopCalled);
        }

        public void UpdateChat()
        {
            // Init db
            DataContext db = new DataContext();

            // Get all online ids
            List<string> onlineIds = db.Online.ToArray().Select(x => x.Id).ToList();

            // Loop thru onlineids and get friends
            foreach (var userId in onlineIds)
            {
                // Get username
                User user = db.Users.Find(userId);
                string username = user.Username;

                // Get all friend ids

                List<string> friendIds1 = db.Friends.Where(x => x.User1 == userId && x.Active == true).ToArray().Select(x => x.User2).ToList();

                List<string> friendIds2 = db.Friends.Where(x => x.User2 == userId && x.Active == true).ToArray().Select(x => x.User1).ToList();

                List<string> allFriendsIds = friendIds1.Concat(friendIds2).ToList();

                // Get final set of ids
                List<string> resultList = onlineIds.Where((i) => allFriendsIds.Contains(i)).ToList();

                // Create a dict of friend ids and usernames

                Dictionary<string, string> dictFriends = new Dictionary<string, string>();

                foreach (var id in resultList)
                {
                    var users = db.Users.Find(id);
                    string friend = users.Username;

                    if (!dictFriends.ContainsKey(id))
                    {
                        dictFriends.Add(id, friend);
                    }
                }

                var transformed = from key in dictFriends.Keys
                                  select new { id = key, friend = dictFriends[key] };

                string json = JsonConvert.SerializeObject(transformed);

                // Set clients
                var clients = Clients.All;

                // Call js function
                clients.updatechat(username, json);
            }

        }

        public void SendChat(int friendId, string friendUsername, string message)
        {
            // Init db
            DataContext db = new DataContext();

            // Get user id
            User userDTO = db.Users.Where(x => x.Username.Equals(Context.User.Identity.Name)).FirstOrDefault();
            string userId = userDTO.Id;

            // Set clients
            var clients = Clients.All;

            // Call js function
            clients.sendchat(userId, Context.User.Identity.Name, friendId, friendUsername, message);
        }
    }
}