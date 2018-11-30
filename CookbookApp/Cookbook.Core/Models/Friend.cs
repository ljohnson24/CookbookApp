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
    public class Friend:BaseEntity
    {
        public int User1 { get; set; }
        public int User2 { get; set; }
        public bool Active { get; set; }

        [ForeignKey("User1")]
        public virtual User Users1 { get; set; }

        [ForeignKey("User2")]
        public virtual User Users2 { get; set; }
    }
}
