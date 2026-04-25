using ProjetoP2.Register.Domain.ValueObjects;
using ProjetoP2.Shared.Entities;
using ProjetoP2.Shared.ValueObjects;

namespace ProjetoP2.Register.Domain.Entities
{
    public class Pet : BaseEntity
    {
        public string Name { get; set; }

        public PetRG? PetRG { get; set; }

        public Color? Color { get; set; }
        public Specie Specie { get; set; }
        public Sex Sex { get; set; }
        public bool Castrated { get; set; }
        public bool? Community { get; set; }

        // Alterar 
        public bool? Microchipped { get; set; }
        public int? MicrochippedNumber { get; set; }

        public DateTime BirthDate { get; set; }

        // Naturalidade
        public State State { get; set; }
        public City City { get; set; }

        public string? PhotoURL { get; set; }

        public Guid OwnerId { get; private set; }
        public Owner Owner { get; private set; }

        public Pet(string name, PetRG petRG, Color color, Specie specie, Sex sex, bool castrated, bool community, bool microchipped, int microchippedNumber, DateTime birthDate, State state, City city, string photoURL, Guid ownerId, Owner owner)
        {
            this.Name = name;
            this.PetRG = petRG;
            this.Color = color;
            this.Specie = specie;
            this.Sex = sex;
            this.Castrated = castrated;
            this.Community = community;
            this.Microchipped = microchipped;
            this.MicrochippedNumber = microchippedNumber;
            this.BirthDate = birthDate;
            this.State = state;
            this.City = city;
            this.PhotoURL = photoURL;
            this.OwnerId = ownerId;
            this.Owner = owner;
        }
    }
}
