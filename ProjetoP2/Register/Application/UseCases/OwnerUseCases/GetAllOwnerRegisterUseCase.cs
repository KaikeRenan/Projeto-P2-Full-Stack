using ProjetoP2.Register.Application.DTOs.Owner;
using ProjetoP2.Register.Domain.IRepositories;

namespace ProjetoP2.Register.Application.UseCases.OwnerUseCases
{
    public class GetAllOwnerRegisterUseCase
    {
        private readonly IOwnerRegisterRepository _ownerRepository;

        public GetAllOwnerRegisterUseCase(IOwnerRegisterRepository owerRepository)
        {
            _ownerRepository = owerRepository;
        }

        public async Task<List<ResponseOwnerRegisterDto>> Run()
        {
            var owners = await _ownerRepository.GetAllAsync();

            return owners.Select(owner => new ResponseOwnerRegisterDto
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Email = owner.Email.Value,
            }).ToList();
        }
    }
}
