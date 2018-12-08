using Cookbook.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Core.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        
        public UserViewModel() { }
        public UserViewModel(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            EmailAddress = user.EmailAddress;
            Password = user.Password;
        }

    }
}
