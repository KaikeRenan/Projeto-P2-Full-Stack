using ProjetoP2.Register.Application.DTOs.Pet;
using ProjetoP2.Register.Domain.IRepositories;

namespace ProjetoP2.Register.Application.UseCases.PetUseCases
{
    public class GetPetRegisterUseCase
    {
        private readonly IPetRegisterRepository _petRepository;
        
        public GetPetRegisterUseCase(IPetRegisterRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public List<ResponsePetRegisterDto> Run()
        {
            return _petRepository.GetAll().Select(pet => new ResponsePetRegisterDto
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
