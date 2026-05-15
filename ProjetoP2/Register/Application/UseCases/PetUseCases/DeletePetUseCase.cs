using ProjetoP2.Register.Domain.IRepositories;

namespace ProjetoP2.Register.Application.UseCases.PetUseCases
{
    public class DeletePetUseCase
    {
        private readonly IPetRepository _petRepository;

        public DeletePetUseCase(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public void Run(Guid id) 
        {
            var pet = _petRepository.GetById(id);

            if (pet == null)
                throw new Exception("Pet não encontrado");

            _petRepository.Delete(pet);
        }
    }
}
