using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Core.Models
{
    public class Wall:BaseEntity
    {
        public string Message { get; set; }
        public DateTime DateEdited { get; set; }

        [ForeignKey("Id")]
        public virtual User Users { get; set; }
    }
}
