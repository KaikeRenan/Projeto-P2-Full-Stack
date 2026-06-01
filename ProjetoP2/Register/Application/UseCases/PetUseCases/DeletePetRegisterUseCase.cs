using ProjetoP2.Register.Domain.IRepositories;

namespace ProjetoP2.Register.Application.UseCases.PetUseCases
{
    public class DeletePetRegisterUseCase
    {
        private readonly IPetRegisterRepository _petRepository;

        public DeletePetRegisterUseCase(IPetRegisterRepository petRepository)
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
