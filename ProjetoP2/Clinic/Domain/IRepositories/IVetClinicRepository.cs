using ProjetoP2.Clinic.Domain.Entities;
using ProjetoP2.Shared.IRepositories;

namespace ProjetoP2.Clinic.Domain.IRepositories
{
    public interface IVetClinicRepository : IBaseRepository<VetClinic>
    {
        Task<bool> ExistsByCpfAsync(string cpf);
        Task<bool> ExistsByEmailAsync(string email);
        Task<bool> ExistsByCrmvAsync(string crmv);
    }
}
