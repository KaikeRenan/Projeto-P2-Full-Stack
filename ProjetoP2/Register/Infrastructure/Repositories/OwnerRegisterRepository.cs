using Microsoft.EntityFrameworkCore;
using ProjetoP2.Infrastructure.Data.Context;
using ProjetoP2.Register.Domain.Entities;
using ProjetoP2.Register.Domain.IRepositories;
using ProjetoP2.Shared.Repositories;

namespace ProjetoP2.Register.Infrastructure.Repositories
{
    public class OwnerRegisterRepository : BaseRepository<OwnerRegister>, IOwnerRegisterRepository
    {
        private readonly Context _context;

        public OwnerRegisterRepository(Context context) : base(context) 
        {
            _context = context;
        }

        public async Task<bool> ExistsByCpfAsync(string cpf)
        {
            return await _context.OwnerRegisters
                .AnyAsync(entity => entity.CPF.Value == cpf);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.OwnerRegisters
                .AnyAsync(entity => entity.Email.Value == email);
        }
    }
}
