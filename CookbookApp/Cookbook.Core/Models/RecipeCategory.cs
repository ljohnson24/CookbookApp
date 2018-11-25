using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Core.Models
{
    public class RecipeCategory
    {
        public string Id { get; set; }
        public string Category { get; set; }

        public RecipeCategory()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
