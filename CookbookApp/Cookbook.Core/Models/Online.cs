using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Core.Models
{
    public class Online:BaseEntity
    {
        public string ConnId { get; set; }
        public virtual User Users { get; set; }
    }
}
