using ProjetoP2.Shared.Entities;

namespace ProjetoP2.Clinic.Domain.Entities
{
    public class AppointmentClinic : BaseEntity
    {
        public Guid VetId { get; private set; }
        public Guid PetId { get; private set; }
        public DateTime DateAppointment { get; private set; }
        public string? Notes { get; private set; }

        public AppointmentClinic(Guid vetId, Guid petId, DateTime dateAppointment)
        {
            this.VetId = vetId;
            this.PetId = petId;
            this.DateAppointment = dateAppointment;
        }
        public void ChangeVet(Guid newVetId)
        {
            VetId = newVetId;
        }

        public void Reschedule(DateTime newDate)
        {
            DateAppointment = newDate;
        }

        public void UpdateNotes(string notes)
        {
            Notes = notes;
        }
    }
}
