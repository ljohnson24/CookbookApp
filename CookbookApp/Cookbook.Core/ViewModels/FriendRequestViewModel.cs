using Cookbook.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Core.ViewModels
{
    public class FriendRequestViewModel
    {
        public string User1 { get; set; }
        public string User2 { get; set; }
        public bool Active { get; set; }

        public FriendRequestViewModel() { }
        public FriendRequestViewModel(Friend friend)
        {
            User1 = friend.User1;
            User2 = friend.User2;
            Active = friend.Active;
        }
        
    }
}
