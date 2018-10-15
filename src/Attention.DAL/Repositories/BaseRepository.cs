using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attention.DAL.Repositories
{
    public class BaseRepository<TEntity> where TEntity : class
    {
        private DbContext _context;
        private DbSet<TEntity> _entity;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _entity = _context.Set<TEntity>();
        }

        public IList<TEntity> GetAll()
        {
            return _entity.ToList();
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await _entity.ToListAsync();
        }

        public EntityEntry<TEntity> Insert(TEntity model)
        {
            return _entity.Add(model);
        }

        public async Task<EntityEntry<TEntity>> InsertAsync(TEntity model)
        {
            return await _entity.AddAsync(model);
        }

        public EntityEntry<TEntity> Delete(TEntity model)
        {
            return _entity.Remove(model);
        }
    }
}
