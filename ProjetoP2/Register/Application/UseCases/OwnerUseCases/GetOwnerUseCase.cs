using ProjetoP2.Register.Application.DTOs.Owner;
using ProjetoP2.Register.Domain.IRepositories;

namespace ProjetoP2.Register.Application.UseCases.OwnerUseCases
{
    public class GetOwnerUseCase
    {
        private readonly IOwnerRepository _ownerRepository;

        public GetOwnerUseCase(IOwnerRepository owerRepository)
        {
            _ownerRepository = owerRepository;
        }

        public List<ResponseOwnerDto> Run()
        {
            return _ownerRepository.GetAll().Select(owner => new ResponseOwnerDto
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Email = owner.Email.Value,
            }).ToList();
        }
    }
}
