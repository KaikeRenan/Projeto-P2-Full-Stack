using ProjetoP2.Register.Application.DTOs.Owner;
using ProjetoP2.Register.Domain.IRepositories;

namespace ProjetoP2.Register.Application.UseCases.OwnerUseCases
{
    public class GetByIdOwnerRegisterUseCase
    {
        private readonly IOwnerRegisterRepository _ownerRepository;

        public GetByIdOwnerRegisterUseCase(IOwnerRegisterRepository owerRepository)
        {
            this._ownerRepository = owerRepository;
        }

        public async Task<ResponseOwnerRegisterDto> Run(Guid Id)
        {
            var owner = await _ownerRepository.GetByIdAsync(Id);

            if (owner == null)
                throw new Exception("Dono não foi encontrado");

            return new ResponseOwnerRegisterDto
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Email = owner.Email.Value,
            };
        }
    }
}
