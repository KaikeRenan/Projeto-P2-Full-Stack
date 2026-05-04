using ProjetoP2.Shared.Entities;

namespace ProjetoP2.Clinic.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public Guid VetId { get; private set; }
        public Guid PetId { get; private set; }
        public DateTime DateAppointment { get; private set; }

        public string? Notes { get; private set; }

        public Appointment(Guid vetId, Guid petId, DateTime dateAppointment)
        {
            this.VetId = vetId;
            this.PetId = petId;
            this.DateAppointment = dateAppointment;
        }
    }
}
