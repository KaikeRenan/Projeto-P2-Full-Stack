using ProjetoP2.Shared.Entities;

namespace ProjetoP2.Register.Domain.Entities
{
    public class AppointmentRegister : BaseEntity
    {
        public Guid PetId { get; private set; }
        public DateTime DateAppointment { get; private set; }
        public string? Notes { get; private set; }

        protected AppointmentRegister() { }

        public AppointmentRegister(Guid vetId, Guid petId, DateTime dateAppointment)
        {
            PetId = petId;
            DateAppointment = dateAppointment;
        }

        public void Reschedule(DateTime newDate)
        {
            DateAppointment = newDate;
        }
    }
}
