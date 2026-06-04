
using ProjetoP2.Clinic.Application.DTOs.Appointment;
using ProjetoP2.Clinic.Domain.IRepositories;


namespace ProjetoP2.Clinic.Application.UseCases.Appointment
{
    public class GetAppointmentClinicUseCase
    {
        private readonly IAppointmentClinicRepository _AppointmentRepository;

        public GetAppointmentClinicUseCase(IAppointmentClinicRepository AppointmentRepository)
        {
            _AppointmentRepository = AppointmentRepository;
        }

        public List<ResponseAppointmentClinicDto> Run()
        {
            return _AppointmentRepository.GetAll().Select(appointment => new ResponseAppointmentClinicDto
            {
                Id = appointment.Id,
                VetId = appointment.VetId,
                PetId = appointment.PetId,
                DateAppointment = appointment.DateAppointment
            }).ToList();
        }
    }
}
