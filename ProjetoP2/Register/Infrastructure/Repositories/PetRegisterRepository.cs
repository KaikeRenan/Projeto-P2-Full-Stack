using ProjetoP2.Infrastructure.Data.Context;
using ProjetoP2.Register.Domain.Entities;
using ProjetoP2.Register.Domain.IRepositories;
using ProjetoP2.Shared.Repositories;

namespace ProjetoP2.Register.Infrastructure.Repositories
{
    public class PetRegisterRepository : BaseRepository<PetRegister>, IPetRegisterRepository
    {
        public PetRegisterRepository(Context context) : base(context) { }
    }
}
