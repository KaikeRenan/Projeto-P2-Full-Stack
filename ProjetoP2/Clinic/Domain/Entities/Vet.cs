using ProjetoP2.Clinic.Domain.ValueObjects;
using ProjetoP2.Shared.Entities;
using ProjetoP2.Shared.ValueObjects;

namespace ProjetoP2.Clinic.Domain.Entities
{
    public class Vet : BaseEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public CPF CPF { get; private set; }

        public CRMV CRMV { get; private set; }

        public ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();

        public Vet(string firstName, string lastName, Email email, PhoneNumber phoneNumber, CPF cpf, CRMV crmv)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.CPF = cpf;
            this.CRMV = crmv;
        }

        public void AddPet(Appointment pet) { Appointments.Add(pet); }
    }
}
