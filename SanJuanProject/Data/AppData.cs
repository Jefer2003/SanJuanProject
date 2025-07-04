using Microsoft.EntityFrameworkCore;
using SanJuanProject.Models;
using System.Collections.Generic;

namespace SanJuanProject.Data
{
    public class AppData
    {
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

            public DbSet<Docente> docente { get; set; }
            public DbSet<Curso> curso { get; set; }
        }
    }
}
