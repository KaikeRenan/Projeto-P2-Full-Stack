using ProjetoP2.Register.Application.DTOs.Pet;
using ProjetoP2.Register.Domain.Entities;
using ProjetoP2.Register.Domain.IRepositories;
using ProjetoP2.Register.Domain.ValueObjects;
using ProjetoP2.Shared.ValueObjects;

namespace ProjetoP2.Register.Application.UseCases.PetUseCases
{
    public class CreatePetRegisterUseCase
    {
        private readonly IPetRegisterRepository _petRepository;
        private readonly IOwnerRegisterRepository _ownerRepository;

        public CreatePetRegisterUseCase (IPetRegisterRepository petRepository, IOwnerRegisterRepository ownerRepository)
        {
            _petRepository = petRepository;
            _ownerRepository = ownerRepository;
        }

        public ResponsePetRegisterDto Run(CreatePetRegisterDto dto)
        {
            OwnerRegister? owner = null;

            if (dto.OwnerId.HasValue)
            {
                owner = _ownerRepository.GetById(dto.OwnerId.Value);

                if (owner == null) 
                    throw new Exception("Dono não foi encontrado");
            }

            var pet = new PetRegister(
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

            _petRepository.Create(pet);

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
