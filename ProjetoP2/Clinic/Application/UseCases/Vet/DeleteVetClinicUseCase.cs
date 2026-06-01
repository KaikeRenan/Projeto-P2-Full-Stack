using ProjetoP2.Clinic.Domain.IRepositories;

namespace ProjetoP2.Clinic.Application.UseCases.Vet
{
    public class DeleteVetClinicUseCase
    {
        private readonly IVetClinicRepository _vetRepository;

        public DeleteVetClinicUseCase(IVetClinicRepository vetRepository)
        {
            _vetRepository = vetRepository;
        }

        public void Run(Guid Id)
        {
            var vet = _vetRepository.GetById(Id);

            if (vet == null)
                throw new Exception("Veterinário não foi encontrado");

            _vetRepository.Delete(vet);
        }
    }
}
