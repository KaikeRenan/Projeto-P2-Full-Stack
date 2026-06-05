using ProjetoP2.Shared.Entities;

namespace ProjetoP2.Shared.IRepositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        public Task<List<T>> GetAllAsync();
        public Task<T?> GetByIdAsync(Guid Id);
        public Task CreateAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
    }
}
