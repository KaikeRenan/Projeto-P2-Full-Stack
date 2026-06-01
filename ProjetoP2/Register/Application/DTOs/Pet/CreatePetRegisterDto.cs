namespace ProjetoP2.Register.Application.DTOs.Pet
{
    public class CreatePetRegisterDto
    {
        public string Name { get; set; }

        public string? PetRG { get; set; }

        public string Color { get; set; }

        public string Specie { get; set; }

        public string Sex { get; set; }

        public bool Castrated { get; set; }

        public bool Community { get; set; }

        public bool Microchipped { get; set; }

        public int? MicrochippedNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public string? State { get; set; }

        public string? City { get; set; }

        public string? PhotoURL { get; set; }

        public Guid? OwnerId { get; set; }
    }
}
