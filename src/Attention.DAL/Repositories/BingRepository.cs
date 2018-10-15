using Attention.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attention.DAL.Repositories
{
    public class BingRepository : BaseRepository<Bing>
    {
        public BingRepository(DbContext context) : base(context)
        {
        }
    }
}
