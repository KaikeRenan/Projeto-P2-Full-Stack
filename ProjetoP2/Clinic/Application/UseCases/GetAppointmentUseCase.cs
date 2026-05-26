using ProjetoP2.Register.Application.DTOs.Owner;
using ProjetoP2.Register.Domain.IRepositories;

namespace ProjetoP2.Register.Application.UseCases.OwnerUseCases
{
    public class GetOwnerUseCase
    {
        private readonly IOwnerRepository _owerRepository;

        public GetOwnerUseCase(IOwnerRepository owerRepository)
        {
            _owerRepository = owerRepository;
        }

        public List<ResponseAppointmentDto> Run()
        {
            return _owerRepository.GetAll().Select(owner => new ResponseAppointmentDto
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Email = owner.Email.Value,
            }).ToList();
        }
    }
}
