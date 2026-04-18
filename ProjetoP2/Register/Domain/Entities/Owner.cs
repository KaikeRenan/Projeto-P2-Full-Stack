using ProjetoP2.Shared.Entities;
using ProjetoP2.Shared.ValueObjects;

namespace ProjetoP2.Register.Domain.Entities
{
    public class Owner : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Email Email { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public CPF CPF { get; set; }

        public ICollection<Pet> Pets { get; set; } = new List<Pet>();

        public Owner(string firstName, string lastName, Email email, PhoneNumber phoneNumber, CPF cpf)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.CPF = cpf;
        }
    }
}
