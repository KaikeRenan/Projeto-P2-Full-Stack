using ProjetoP2.Clinic.Application.DTOs.Appointment;
using ProjetoP2.Clinic.Domain.IRepositories;
using ProjetoP2.Infrastructure.Data;

namespace ProjetoP2.Clinic.Application.UseCases.GetAppointmentUseCase
{
    public class GetAppointmentUseCase
    {
        private readonly IAppointmentRepository _AppointmentRepository;

        public GetAppointmentUseCase(IAppointmentRepository AppointmentRepository)
        {
            _AppointmentRepository = AppointmentRepository;
        }

        public List<ResponseAppointmentDto> Run()
        {
            return _AppointmentRepository.GetAll().Select(appointment => new ResponseAppointmentDto
            {
                Id = appointment.Id,
                VetId = appointment.VetId,
                PetId = appointment.PetId,
                DateAppointment = appointment.DateAppointment
            }).ToList();
        }
    }
}
