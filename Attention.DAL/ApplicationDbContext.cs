using Attention.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attention.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Bing> Bings { get; set; }

        public DbSet<Ioliu> Iolius { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
