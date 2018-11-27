using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ToDoListApp.Models
{
    public class ToDoListAppContext : DbContext
    {
        public ToDoListAppContext(DbContextOptions<ToDoListAppContext> options)
            : base(options)
        {

        }

        public DbSet<ToDoListApp.Models.ListEntry> ListEntry { get; set; }
        public DbSet<ToDoListApp.Models.User> Users { get; set; }
    }
}
