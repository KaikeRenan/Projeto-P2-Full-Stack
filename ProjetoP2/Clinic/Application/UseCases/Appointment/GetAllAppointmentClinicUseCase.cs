using ProjetoP2.Clinic.Application.DTOs.Appointment;
using ProjetoP2.Clinic.Domain.IRepositories;

namespace ProjetoP2.Clinic.Application.UseCases.Appointment
{
    public class GetAllAppointmentClinicUseCase
    {
        private readonly IAppointmentClinicRepository _appointmentRepository;

        public GetAllAppointmentClinicUseCase(IAppointmentClinicRepository appointmentRepository)
        {
            this._appointmentRepository = appointmentRepository;
        }

        public async Task<List<ResponseAppointmentClinicDto>> Run()
        {
            var appointments = await _appointmentRepository.GetAllAsync();

            return appointments.Select(appointment => new ResponseAppointmentClinicDto
            {
                Id = appointment.Id,
                VetId = appointment.VetId,
                PetId = appointment.PetId,
                DateAppointment = appointment.DateAppointment,
                Notes = appointment.Notes
            }).ToList();
        }
    }
}
