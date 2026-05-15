using ProjetoP2.Register.Application.DTOs.Pet;
using ProjetoP2.Register.Domain.IRepositories;

namespace ProjetoP2.Register.Application.UseCases.PetUseCases
{
    public class GetPetUseCase
    {
        private readonly IPetRepository _petRepository;
        
        public GetPetUseCase(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public List<ResponsePetDto> Run()
        {
            return _petRepository.GetAll().Select(pet => new ResponsePetDto
            {
                Id = pet.Id,
                Name = pet.Name,
                Specie = pet.Specie.Value,
                OwnerId = pet.OwnerId
            })
                .ToList();
        }
    }
}
