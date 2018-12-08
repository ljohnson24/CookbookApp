using Cookbook.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Core.ViewModels
{
    public class MessageViewModel
    {
        public Messenger Message { get; set; }

        public MessageViewModel() { }
        public MessageViewModel(Messenger message)
        {
            Message = message;
        }
    }
}
