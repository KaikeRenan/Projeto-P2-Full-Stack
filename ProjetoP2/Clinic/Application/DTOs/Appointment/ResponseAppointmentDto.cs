namespace ProjetoP2.Clinic.Application.DTOs.Appointment
{
    public class ResponseAppointmentDto
    {
        public Guid Id { get; set; }
        public Guid VetId { get; private set; }
        public Guid PetId { get; private set; }
        public DateTime DateAppointment { get; private set; }


    }
}
