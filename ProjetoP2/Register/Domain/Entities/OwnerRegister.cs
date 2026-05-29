using ProjetoP2.Shared.Entities;
using ProjetoP2.Shared.ValueObjects;

namespace ProjetoP2.Register.Domain.Entities
{
    public class OwnerRegister : BaseEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public CPF CPF { get; private set; }

        public ICollection<PetRegister> Pets { get; private set; } = new List<PetRegister>();

        protected OwnerRegister() { }
        public OwnerRegister(string firstName, string lastName, Email email, PhoneNumber phoneNumber, CPF cpf)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.CPF = cpf;
        }

        public void AddPet(PetRegister pet) { Pets.Add(pet); }
    }
}
