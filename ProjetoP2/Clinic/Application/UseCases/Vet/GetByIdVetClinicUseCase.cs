using ProjetoP2.Clinic.Application.DTOs.Vet;
using ProjetoP2.Clinic.Domain.IRepositories;

namespace ProjetoP2.Clinic.Application.UseCases.Vet
{
    public class GetByIdVetClinicUseCase
    {
        private readonly IVetClinicRepository _vetRepository;

        public GetByIdVetClinicUseCase(IVetClinicRepository vetRepository)
        {
            this._vetRepository = vetRepository;
        }

        public async Task<ResponseVetClinicDto> Run(Guid Id)
        {
            var vet = await _vetRepository.GetByIdAsync(Id);

            if (vet == null)
                throw new Exception("Veterinário não encontrado");

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
