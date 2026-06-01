using ProjetoP2.Clinic.Application.DTOs.Appointment;
using ProjetoP2.Clinic.Domain.IRepositories;
using ProjetoP2.Infrastructure.Data.Entities;

namespace ProjetoP2.Clinic.Application.UseCases.Vet
{
    public class GetVetClinicUseCase
    {
        private readonly IVetClinicRepository _vetRepository;

        public GetVetClinicUseCase(IVetClinicRepository vetRepository)
        {
            _vetRepository = vetRepository;
        }

        public List<GetVetClinicUseCase> Run()
        {
            return _vetRepository.GetAll().Select(vet => new ResponseVetClinicDto
            {
                Id = vet.Id,
                FirstName = vet.FirstName,
                LastName = vet.LastName,
                Email = vet.Email.Value,
                PhoneNumber = vet.PhoneNumber.Value,
                CPF = vet.CPF.Value,
                CRMV = vet.CRMV.Value,
            }).ToList();
        }
    }
}
