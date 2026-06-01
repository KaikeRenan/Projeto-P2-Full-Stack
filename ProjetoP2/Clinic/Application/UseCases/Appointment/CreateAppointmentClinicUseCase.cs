using ProjetoP2.Clinic.Application.DTOs.Appointment;
using ProjetoP2.Clinic.Domain.Entities;
using ProjetoP2.Clinic.Domain.IRepositories;

namespace ProjetoP2.Clinic.Application.UseCases.Appointment
{
    public class CreateAppointmentClinicUseCase
    {
        private readonly IAppointmentClinicRepository _appointmentRepository;

        public CreateAppointmentClinicUseCase(IAppointmentClinicRepository appointmentRepository)
        {
            this._appointmentRepository = appointmentRepository;
        }

        public ResponseVetClinicDto Run(CreateAppointmentClinicDto dto)
        {
            var appointment = new AppointmentClinic(
                dto.VetId,
                dto.PetId,
                dto.DateAppointment
            );

            _appointmentRepository.Create(appointment);

            return new ResponseVetClinicDto
            {
                VetId = appointment.VetId,
                PetId = appointment.PetId,
                DateAppointment = appointment.DateAppointment
            };
        }
    }
}
