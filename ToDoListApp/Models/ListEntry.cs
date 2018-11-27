using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApp.Models
{
    public class ListEntry
    {
        public int ID { get; set; }
        public string Entry { get; set; }

        public int UserID { get; set; } // external key
        public User User { get; set; } // navigation property
    }
}
