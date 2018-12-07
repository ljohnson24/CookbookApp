using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Core.Models
{
    public class Friend:BaseEntity
    {
        public string User1 { get; set; }
        public string User2 { get; set; }
        public bool Active { get; set; }

        public virtual User Users1 { get; set; }
        public virtual User Users2 { get; set; }
    }
}
