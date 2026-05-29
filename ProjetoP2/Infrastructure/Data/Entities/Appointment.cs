using ProjetoP2.Shared.Entities;

namespace ProjetoP2.Infrastructure.Data.Entities
{
    public class Appointment : BaseEntity
    {
        public Guid VetId { get; private set; }
        public Guid PetId { get; private set; }
        public DateTime DateAppointment { get; private set; }
        public string? Notes { get; private set; }

        protected Appointment() { }

        public Appointment(Guid vetId, Guid petId, DateTime dateAppointment, string? notes = null)
        {
            VetId = vetId;
            PetId = petId;
            DateAppointment = dateAppointment;
            Notes = notes;
        }

        public void Reschedule(DateTime newDate) => DateAppointment = newDate;
        public void UpdateNotes(string? notes) => Notes = notes;
        public void ChangeVet(Guid newVetId) => VetId = newVetId;
    }
}

