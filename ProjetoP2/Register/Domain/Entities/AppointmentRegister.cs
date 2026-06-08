using ProjetoP2.Shared.Entities;

namespace ProjetoP2.Register.Domain.Entities
{
    public class AppointmentRegister : BaseEntity
    {
        public Guid VetId { get; private set; }
        public Guid PetId { get; private set; }
        public DateTime DateAppointment { get; private set; }

        protected AppointmentRegister() { }

        public AppointmentRegister(Guid vetId, Guid petId, DateTime dateAppointment)
        {
            VetId = vetId;
            PetId = petId;
            DateAppointment = dateAppointment;
        }

        internal void SetId(Guid id)
        {
            Id = id;
        }

        public void Reschedule(DateTime newDate) => DateAppointment = newDate;

        public void Update(Guid vetId, Guid petId, DateTime dateAppointment)
        {
            VetId = vetId;
            PetId = petId;
            DateAppointment = dateAppointment;
        }
    }
}

