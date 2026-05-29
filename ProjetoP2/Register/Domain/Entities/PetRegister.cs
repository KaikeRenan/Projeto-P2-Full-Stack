using ProjetoP2.Register.Domain.ValueObjects;
using ProjetoP2.Shared.Entities;
using ProjetoP2.Shared.ValueObjects;

namespace ProjetoP2.Register.Domain.Entities
{
    public class PetRegister : BaseEntity
    {
        public string Name { get; private set; }

        public PetRG? PetRG { get; private set; }

        public Color Color { get; private set; }
        public Specie Specie { get; private set; }
        public Sex Sex { get; private set; }
        public bool Castrated { get; private set; }
        public bool Community { get; private set; }

        public bool Microchipped { get; private set; }
        public int? MicrochippedNumber { get; private set; }
        public DateTime BirthDate { get; private set; }
        public State? State { get; private set; }
        public City? City { get; private set; }

        public string? PhotoURL { get; private set; }

        public Guid? OwnerId { get; private set; }
        public OwnerRegister? Owner { get; private set; }
        protected PetRegister() { }
        public PetRegister(
            string name,
            Color color,
            Specie specie, 
            Sex sex, 
            bool castrated, 
            bool community,
            bool microchipped,
            DateTime birthDate,
            PetRG? petRG = null,
            int? microchippedNumber = null,
            State? state = null,
            City? city = null,
            string? photoURL = null,
            Guid? ownerId = null
            )
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Nome do pet é obrigatório");

            if (microchipped && microchippedNumber == null)
                throw new ArgumentException("Número do microship é obrigatório");

            if (!microchipped)
                microchippedNumber = null;

            if (petRG != null && ownerId == null)
                throw new ArgumentException("Pets com RG devem possuir Dono");

            this.Name = name;
            this.Color = color;
            this.Specie = specie;
            this.Sex = sex;
            this.Castrated = castrated;
            this.Community = community;
            this.Microchipped = microchipped;
            this.BirthDate = birthDate;
            this.PetRG = petRG;
            this.MicrochippedNumber = microchippedNumber;
            this.State = state;
            this.City = city;
            this.PhotoURL = photoURL;
            this.OwnerId = ownerId;
        }
    }
}
