using Attention.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attention.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Hippie> Hippies { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }
    }
}
