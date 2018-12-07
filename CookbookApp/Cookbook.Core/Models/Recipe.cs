using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Core.Models
{
    public class Recipe:BaseEntity
    {
        [StringLength(20)] //limit character length
        [DisplayName("Recipe Name")] //display character name in UI
        public string Name { get; set; }
        
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public double ServingSize { get; set; }
        public int Calories { get; set; }
        public string ingredient1 { get; set; }
        public string ingredient2 { get; set; }
        public string ingredient3 { get; set; }
        public string ingredient4 { get; set; }
        public string ingredient5 { get; set; }
        public string ingredient6 { get; set; }
        public string ingredient7 { get; set; }
        public string ingredient8 { get; set; }
        public string ingredient9 { get; set; }
        public string ingredient10 { get; set; }
        public string instruction1 { get; set; }
        public string instruction2 { get; set; }
        public string instruction3 { get; set; }
        public string instruction4 { get; set; }
        public string instruction5 { get; set; }
        public string instruction6 { get; set; }
        public string instruction7 { get; set; }
        public string instruction8 { get; set; }
        public string instruction9 { get; set; }
        public string instruction10 { get; set; }
        
    }
}
