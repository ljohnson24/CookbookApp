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
    public class Messenger:BaseEntity
    {
        public int From { get; set; }
        public int To { get; set; }
        public string Message { get; set; }
        public DateTime DateSent { get; set; }
        public bool Read { get; set; }
        
        [ForeignKey("From")]
        public virtual User FromUsers { get; set; }

        [ForeignKey("To")]
        public virtual User ToUsers { get; set; }

    }
}
