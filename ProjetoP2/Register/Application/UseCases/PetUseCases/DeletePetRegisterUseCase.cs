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

        public async Task Run(Guid Id) 
        {
            var pet = await _petRepository.GetByIdAsync(Id);

            if (pet == null)
                throw new Exception("Pet não encontrado");

            await _petRepository.DeleteAsync(pet);
        }
    }
}
