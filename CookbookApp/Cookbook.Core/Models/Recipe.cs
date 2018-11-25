﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Core.Models
{
    public class Recipe
    {
        public string Id { get; set; }

        [StringLength(20)] //limit character length
        [DisplayName("Recipe Name")] //display character name in UI
        public string Name { get; set; }

        [Range(0,1000)]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }

        //contructor
        public Recipe()
        {
            //generate a guid (random number generator) for id
            this.Id = Guid.NewGuid().ToString();
        }
    }
}