using Cookbook.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Core.ViewModels
{
    public class WallViewModel
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public DateTime DateEdited { get; set; }

        public WallViewModel()
        {

        }

        public WallViewModel(Wall wall)
        {
            Id = wall.Id;
            Message = wall.Message;
            DateEdited = wall.DateEdited;

        }
    }
}
