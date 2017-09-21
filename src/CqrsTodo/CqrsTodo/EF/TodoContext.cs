using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CqrsTodo.Models;
using Microsoft.EntityFrameworkCore;

namespace CqrsTodo.EF
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {

        }
        public DbSet<Todo> Todos { get; set; }
    }
}
