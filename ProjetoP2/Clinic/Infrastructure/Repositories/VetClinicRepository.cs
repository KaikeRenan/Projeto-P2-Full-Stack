using Microsoft.EntityFrameworkCore;
using ProjetoP2.Clinic.Domain.Entities;
using ProjetoP2.Clinic.Domain.IRepositories;
using ProjetoP2.Infrastructure.Data.Context;
using ProjetoP2.Shared.Repositories;

namespace ProjetoP2.Clinic.Infrastructure.Repositories
{
    public class VetClinicRepository : BaseRepository<VetClinic>, IVetClinicRepository
    {
        private readonly Context _context;

        public VetClinicRepository(Context context) : base(context) 
        { 
            _context = context;
        }

        public async Task<bool> ExistsByCpfAsync(string cpf)
        {
            return await _context.VetClinics
                .AnyAsync(entity => entity.CPF.Value == cpf);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.VetClinics
                .AnyAsync(entity => entity.Email.Value == email);
        }

        public async Task<bool> ExistsByCrmvAsync(string crmv)
        {
            return await _context.VetClinics
                .AnyAsync(entity => entity.CRMV.Value == crmv);
        }
    }
}
