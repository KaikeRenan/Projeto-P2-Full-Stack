using ProjetoP2.Clinic.Application.DTOs.Appointment;
using ProjetoP2.Clinic.Domain.Entities;
using ProjetoP2.Clinic.Domain.IRepositories;
using ProjetoP2.Clinic.Domain.ValueObjects;
using ProjetoP2.Shared.ValueObjects;

namespace ProjetoP2.Clinic.Application.UseCases.Vet
{
    public class CreateVetClinicUseCase
    {
        private readonly IVetClinicRepository _vetRepository;

        public CreateVetClinicUseCase(IVetClinicRepository vetRepository)
        {
            this._vetRepository = vetRepository;
        }

        public ResponseVetClinicDto Run(CreateAppointmentClinicDto dto)
        {
            var vet = new VetClinic(
                dto.FirstName,
                dto.LastName,
                new Email(dto.Email),
                new PhoneNumber(dto.PhoneNumber),
                new CPF(dto.CPF),
                new CRMV(dto.CRMV)
            );

            _vetRepository.Create(vet);

            return new ResponseVetClinicDto
            {
                Id = vet.Id,
                FirstName = vet.FirstName,
                LastName = vet.LastName,
                Email = vet.Email.Value,
                PhoneNumber = vet.PhoneNumber.Value,
                CPF = vet.CPF.Value,
                CRMV = vet.CRMV.Value,
            };
        }
    }
}
