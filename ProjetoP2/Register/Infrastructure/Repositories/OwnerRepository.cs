using ProjetoP2.Register.Domain.Entities;
using ProjetoP2.Register.Domain.IRepositories;
using ProjetoP2.Shared.Repositories;

namespace ProjetoP2.Register.Infrastructure.Repositories
{
    public class OwnerRepository : BaseRepository<OwnerRegister>, IOwnerRepository
    {
        public OwnerRepository(Context context) : base(context) { }
    }
}
