using ProjetoP2.Shared.Entities;

namespace ProjetoP2.Infrastructure.Data
{
    public class Appointment : BaseEntity 
    {
        public Guid VetId { get; private set; }
        public Guid PetId { get; private set; }
        public DateTime DateAppointment { get; private set; }
        public string? Notes { get; private set; }
    }
}
