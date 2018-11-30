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
    public class Online
    {
        public string ConnId { get; set; }

        [ForeignKey("Id")]
        public virtual User Users { get; set; }
    }
}
