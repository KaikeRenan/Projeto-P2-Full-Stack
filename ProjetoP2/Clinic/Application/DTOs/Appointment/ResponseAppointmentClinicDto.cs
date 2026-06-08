namespace ProjetoP2.Clinic.Application.DTOs.Appointment
{
    public class ResponseAppointmentClinicDto
    {
        public Guid Id { get; set; }
        public Guid VetId { get; set; }
        public Guid PetId { get; set; }
        public DateTime DateAppointment { get; set; }
        public string? Notes{ get; set; }
    }
}
