using Microsoft.EntityFrameworkCore;
using NotesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.Persistence
{
    public class NotesDbContext : DbContext
    {
        public NotesDbContext(DbContextOptions<NotesDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}
