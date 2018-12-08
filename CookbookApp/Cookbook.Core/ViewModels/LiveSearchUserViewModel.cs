using Cookbook.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Core.ViewModels
{
    public class LiveSearchUserViewModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        public LiveSearchUserViewModel()
        {

        }
        public LiveSearchUserViewModel(User user)
        {
            UserId = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
        }
        
        
    }
}
