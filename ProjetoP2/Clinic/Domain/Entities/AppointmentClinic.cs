using ProjetoP2.Shared.Entities;

namespace ProjetoP2.Clinic.Domain.Entities
{
    public class AppointmentClinic : BaseEntity
    {
        public Guid VetId { get; private set; }
        public VetClinic? Vet { get; private set; }
        public Guid PetId { get; private set; }
        public DateTime DateAppointment { get; private set; }
        public string? Notes { get; private set; }

        protected AppointmentClinic() { }

        public AppointmentClinic(Guid vetId, Guid petId, DateTime dateAppointment, string? notes)
        {
            VetId = vetId;
            PetId = petId;
            DateAppointment = dateAppointment;
            Notes = notes;
        }

        internal void SetId(Guid id)
        {
            Id = id;
        }

        public void ChangeVet(Guid newVetId) => VetId = newVetId;
        public void Reschedule(DateTime newDate) => DateAppointment = newDate;
        public void UpdateNotes(string notes) => Notes = notes;

        internal void SetVet(VetClinic vet)
        {
            Vet = vet;
            VetId = vet.Id;
        }

        public void Update(Guid vetId, Guid petId, DateTime dateAppointment, string? notes)
        {
            VetId = vetId;
            PetId = petId;
            DateAppointment = dateAppointment;
            Notes = notes;
        }
    }
}
