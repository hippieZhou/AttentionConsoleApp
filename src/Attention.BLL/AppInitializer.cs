using Attention.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Attention.BLL
{
    public class AppInitializer
    {
        private AppDbContext _ctx;

        public AppInitializer(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task SeedAsync()
        {
            await _ctx.Database.MigrateAsync();
        }
    }
}
