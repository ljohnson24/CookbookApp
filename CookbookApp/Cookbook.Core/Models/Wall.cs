using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Core.Models
{
    public class Wall:BaseEntity
    {
        public string Message { get; set; }
        public DateTime DateEdited { get; set; }
        public virtual User Users { get; set; }
    }
}
