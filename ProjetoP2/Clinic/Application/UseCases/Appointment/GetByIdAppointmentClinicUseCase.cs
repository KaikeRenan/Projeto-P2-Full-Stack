using ProjetoP2.Clinic.Application.DTOs.Appointment;
using ProjetoP2.Clinic.Domain.IRepositories;

namespace ProjetoP2.Clinic.Application.UseCases.Appointment
{
    public class GetByIdAppointmentClinicUseCase
    {
        private readonly IAppointmentClinicRepository _appointmentRepository;

        public GetByIdAppointmentClinicUseCase(IAppointmentClinicRepository appointmentRepository)
        {
            this._appointmentRepository = appointmentRepository;
        }

        public async Task<ResponseAppointmentClinicDto> Run(Guid Id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(Id);

            if (appointment == null)
                throw new Exception("Consulta não encontrada");

            return new ResponseAppointmentClinicDto
            {
                Id = appointment.Id,
                VetId = appointment.VetId,
                PetId = appointment.PetId,
                DateAppointment = appointment.DateAppointment,
                Notes = appointment.Notes
            };
        }
    }
}
