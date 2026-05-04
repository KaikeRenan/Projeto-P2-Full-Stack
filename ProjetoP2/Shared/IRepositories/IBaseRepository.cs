using ProjetoP2.Shared.Entities;

namespace ProjetoP2.Shared.IRepositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        public List<T> GetAll();
        public T? GetById(Guid Id);
        public void Create(T entity);
        public void Update(T entity);
        public void Delete(T entity);
    }
}
