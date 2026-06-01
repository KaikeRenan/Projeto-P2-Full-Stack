using ProjetoP2.Register.Application.DTOs.Owner;
using ProjetoP2.Register.Domain.IRepositories;

namespace ProjetoP2.Register.Application.UseCases.OwnerUseCases
{
    public class GetOwnerRegisterUseCase
    {
        private readonly IOwnerRegisterRepository _ownerRepository;

        public GetOwnerRegisterUseCase(IOwnerRegisterRepository owerRepository)
        {
            _ownerRepository = owerRepository;
        }

        public List<ResponseOwnerRegisterDto> Run()
        {
            return _ownerRepository.GetAll().Select(owner => new ResponseOwnerRegisterDto
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Email = owner.Email.Value,
            }).ToList();
        }
    }
}
