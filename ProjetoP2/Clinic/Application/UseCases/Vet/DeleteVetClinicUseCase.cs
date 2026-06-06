using ProjetoP2.Clinic.Domain.IRepositories;

namespace ProjetoP2.Clinic.Application.UseCases.Vet
{
    public class DeleteVetClinicUseCase
    {
        private readonly IVetClinicRepository _vetRepository;

        public DeleteVetClinicUseCase(IVetClinicRepository vetRepository)
        {
            this._vetRepository = vetRepository;
        }

        public async Task Run(Guid Id)
        {
            var vet = await _vetRepository.GetByIdAsync(Id);

            if (vet == null)
                throw new Exception("Veterinário não encontrado");

            await _vetRepository.DeleteAsync(vet);
        }
    }
}
