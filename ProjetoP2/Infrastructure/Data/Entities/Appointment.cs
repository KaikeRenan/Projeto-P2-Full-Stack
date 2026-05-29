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
        public Appointment(Guid vetId, Guid petId, DateTime dateAppointment)
        {
            this.VetId = vetId;
            this.PetId = petId;
            this.DateAppointment = dateAppointment;
        }
    }
}
