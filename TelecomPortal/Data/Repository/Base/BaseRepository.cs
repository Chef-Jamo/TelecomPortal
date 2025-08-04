using Microsoft.EntityFrameworkCore;
using TelecomPortal.Data.Repository.Context;
using TelecomPortal.Data.Repository.Entities.Base;
using TelecomPortal.Data.Repository.Interfaces;

namespace TelecomPortal.Data.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly TelecomPortalContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(TelecomPortalContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            var now = DateTime.UtcNow;
            entity.DateCreated = now;
            entity.DateLastModified = now;

            _dbSet.Add(entity);

            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            entity.DateLastModified = DateTime.UtcNow;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
