using Attention.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Attention.DAL
{
    public class AttentionDbContext : DbContext
    {
        public DbSet<Bing> Bings { get; set; }
        public DbSet<Ioliu> Iolius { get; set; }
        public AttentionDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
