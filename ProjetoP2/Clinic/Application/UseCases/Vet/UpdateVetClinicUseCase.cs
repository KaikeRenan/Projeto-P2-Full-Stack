using ProjetoP2.Clinic.Application.DTOs.Vet;
using ProjetoP2.Clinic.Domain.IRepositories;
using ProjetoP2.Shared.Exceptions;
using ProjetoP2.Shared.ValueObjects;

namespace ProjetoP2.Clinic.Application.UseCases.Vet
{
    public class UpdateVetClinicUseCase
    {
        private readonly IVetClinicRepository _vetRepository;

        public UpdateVetClinicUseCase(IVetClinicRepository vetRepository)
        {
            this._vetRepository = vetRepository;
        }

        public async Task<ResponseVetClinicDto> Run(UpdateVetClinicDto dto)
        {
            var vet = await _vetRepository.GetByIdAsync(dto.Id);

            if (vet == null)
                throw new Exception("Veterinário não encontrado");

            if (vet.Email.Value != dto.Email && await _vetRepository.ExistsByEmailAsync(dto.Email))
                throw new ExceptionDuplicateEmail();

            vet.Update(
                dto.FirstName,
                dto.LastName,
                new Email(dto.Email),
                new PhoneNumber(dto.PhoneNumber)
            );

            await _vetRepository.UpdateAsync(vet);

            return new ResponseVetClinicDto
            {
                Id = vet.Id,
                FirstName = vet.FirstName,
                LastName = vet.LastName,
                Email = vet.Email.Value,
                PhoneNumber = vet.PhoneNumber.Value,
            };
        }
    }
}
