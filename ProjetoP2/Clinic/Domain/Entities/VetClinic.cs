using ProjetoP2.Clinic.Domain.ValueObjects;
using ProjetoP2.Shared.Entities;
using ProjetoP2.Shared.ValueObjects;

namespace ProjetoP2.Clinic.Domain.Entities
{
    public class VetClinic : BaseEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public CPF CPF { get; private set; }

        public CRMV CRMV { get; private set; }

        public ICollection<AppointmentClinic> Appointments { get; private set; } = new List<AppointmentClinic>();

        protected VetClinic() { }

        public VetClinic(string firstName, string lastName, Email email, PhoneNumber phoneNumber, CPF cpf, CRMV crmv)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.CPF = cpf;
            this.CRMV = crmv;
        }

        public void AddPet(AppointmentClinic pet) { Appointments.Add(pet); }

        public void RemovePet(AppointmentClinic pet) { Appointments.Remove(pet); }

        public void Update(string firstName, string lastName, Email email, PhoneNumber phoneNumber)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
        }
    }
}
