using ProjetoP2.Register.Application.DTOs.Pet;
using ProjetoP2.Register.Domain.IRepositories;
using ProjetoP2.Register.Domain.ValueObjects;
using ProjetoP2.Shared.ValueObjects;

namespace ProjetoP2.Register.Application.UseCases.PetUseCases
{
    public class UpdatePetRegisterUseCase
    {
        private readonly IPetRegisterRepository _petRepository;
        private readonly IOwnerRegisterRepository _ownerRepository;

        public UpdatePetRegisterUseCase(IPetRegisterRepository owerRepository, IOwnerRegisterRepository ownerRepository )
        {
            _petRepository = owerRepository;
            _ownerRepository = ownerRepository;
        }

        public async Task<ResponsePetRegisterDto> Run(UpdatePetRegisterDto dto)
        {
            var pet = await _petRepository.GetByIdAsync(dto.Id);

            if (pet == null)
                throw new Exception("Pet não encontrado");

            if (dto.OwnerId.HasValue)
            {
                var owner = await _ownerRepository.GetByIdAsync(dto.OwnerId.Value);

                if (owner == null) 
                    throw new Exception("Dono não encontrado");
            }

            pet.Update(
                dto.Name,
                new Color(dto.Color),
                new Specie(dto.Specie),
                new Sex(dto.Sex),
                dto.Castrated,
                dto.Community,
                dto.Microchipped,
                dto.BirthDate,
                dto.PetRG != null ? new PetRG(dto.PetRG) : null,
                dto.MicrochippedNumber,
                dto.State != null ? new State(dto.State) : null,
                dto.City != null ? new City(dto.City) : null,
                dto.PhotoURL,
                dto.OwnerId
            );

            await _petRepository.UpdateAsync(pet);

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
