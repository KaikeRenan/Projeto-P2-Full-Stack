namespace ProjetoP2.Register.Application.DTOs.AppointmentRegister
{
    public class CreateAppointmentRegisterDto
    {
        public Guid VetId { get; set; }
        public Guid PetId { get; set; }
        public DateTime DateAppointment { get; set; }
    }
}
