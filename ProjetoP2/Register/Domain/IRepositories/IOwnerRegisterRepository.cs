using ProjetoP2.Register.Domain.Entities;
using ProjetoP2.Shared.IRepositories;

namespace ProjetoP2.Register.Domain.IRepositories
{
    public interface IOwnerRegisterRepository : IBaseRepository<OwnerRegister>
    {
        Task<bool> ExistsByCpfAsync(string cpf);
        Task<bool> ExistsByEmailAsync(string email);
    }
}
