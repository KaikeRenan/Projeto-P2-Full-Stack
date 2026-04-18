using ProjetoP2.Register.Domain.ValueObjects;
using ProjetoP2.Shared.ValueObjects;

namespace ProjetoP2.Register.Domain.Entities
{
    public class Pet
    {
        public string Name { get; set; }

        public PetRG? PetRG { get; set; }

        public Color? Color { get; set; }
        public Specie Specie { get; set; }
        public Sex Sex { get; set; }
        public bool Castrated { get; set; }
        public bool? Community { get; set; }

        // alterar 
        public bool? Microchipped { get; set; }
        public int? MicrochippedNumber { get; set; }

        public DateTime BirthDate { get; set; }
        public State State { get; set; }
        public City City { get; set; }

        public string? PhotoURL { get; set; }
    }
}
