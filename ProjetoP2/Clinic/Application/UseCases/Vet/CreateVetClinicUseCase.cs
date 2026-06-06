using ProjetoP2.Clinic.Application.DTOs.Vet;
using ProjetoP2.Clinic.Domain.Entities;
using ProjetoP2.Clinic.Domain.IRepositories;
using ProjetoP2.Clinic.Domain.ValueObjects;
using ProjetoP2.Shared.Exceptions;
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

        public async Task<ResponseVetClinicDto> Run(CreateVetClinicDto dto)
        {
            if (await _vetRepository.ExistsByCpfAsync(dto.CPF))
                throw new ExceptionDuplicateCpf();

            if (await _vetRepository.ExistsByEmailAsync(dto.Email))
                throw new ExceptionDuplicateEmail();

            if (await _vetRepository.ExistsByCrmvAsync(dto.CRMV))
                throw new ExceptionDuplicateCrmv();

            var vet = new VetClinic(
                dto.FirstName,
                dto.LastName,
                new Email(dto.Email),
                new PhoneNumber(dto.PhoneNumber),
                new CPF(dto.CPF),
                new CRMV(dto.CRMV)
            );

            await _vetRepository.CreateAsync(vet);

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
