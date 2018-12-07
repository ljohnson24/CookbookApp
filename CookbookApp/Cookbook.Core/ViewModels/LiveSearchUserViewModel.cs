﻿using System;
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

        public LiveSearchUserViewModel(LiveSearchUserViewModel user)
        {
            UserId = user.UserId;
            FirstName = user.Username;
            LastName = user.LastName;
            Username = user.Username;
        }
    }
}
