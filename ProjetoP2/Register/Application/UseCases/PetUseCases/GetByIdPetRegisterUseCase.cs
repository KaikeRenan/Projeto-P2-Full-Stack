using ProjetoP2.Register.Application.DTOs.Pet;
using ProjetoP2.Register.Domain.IRepositories;

namespace ProjetoP2.Register.Application.UseCases.PetUseCases
{
    public class GetByIdPetRegisterUseCase
    {
        private readonly IPetRegisterRepository _petRepository;

        public GetByIdPetRegisterUseCase(IPetRegisterRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public async Task<ResponsePetRegisterDto> Run(Guid Id)
        {
            var pet = await _petRepository.GetByIdAsync(Id);

            if (pet == null)
                throw new Exception("Pet não foi encontrado");

            return new ResponsePetRegisterDto
            {
                Id = pet.Id,
                Name = pet.Name,
                Specie = pet.Specie.Value,
                Sex = pet.Sex.Value,
                OwnerId = pet.OwnerId,
            };
        }
    }
}
