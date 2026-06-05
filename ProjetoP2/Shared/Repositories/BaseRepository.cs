using Microsoft.EntityFrameworkCore;
using ProjetoP2.Shared.Entities;
using ProjetoP2.Shared.IRepositories;

namespace ProjetoP2.Shared.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(DbContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<T?> GetByIdAsync(Guid Id)
        {
            return await _dbSet.FirstOrDefaultAsync(entity => entity.Id == Id && entity.RemovedAt == null);
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.Where(entity => entity.RemovedAt == null).ToListAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            entity.RemovedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
