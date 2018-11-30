using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cookbook.Core.Models
{
    public class User:BaseEntity
    {
        public string FirstName { get; set;}
        public string LastName { get; set;}
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
