namespace ProjetoP2.Register.Application.DTOs.AppointmentRegister
{
    public class UpdateAppointmentRegisterDto
    {
        public Guid Id { get; set; }
        public Guid VetId { get; set; }
        public Guid PetId { get; set; }
        public DateTime DateAppointment { get; set; }
    }
}
