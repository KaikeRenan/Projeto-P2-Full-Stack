using ProjetoP2.Shared.ValueObjects;
namespace ProjetoP2.Clinic.Application.DTOs.Appointment
{
    public class CreateAppointmentDto
    {
        public Guid VetId { get; private set; }
        public Guid PetId { get; private set; }
        public DateTime DateAppointment { get; private set; }
        public string? Notes { get; private set; }

    }
}
