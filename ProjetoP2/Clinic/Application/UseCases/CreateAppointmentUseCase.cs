using Microsoft.JSInterop.Infrastructure;
using ProjetoP2.Clinic.Application.DTOs.Appointment;
using ProjetoP2.Clinic.Domain.Entities;
using ProjetoP2.Clinic.Domain.IRepositories;
using ProjetoP2.Shared.ValueObjects;

namespace ProjetoP2.Clinic.Application.UseCases.CreateAppointmentUseCase
{
    public class CreateAppointmentUseCase
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public CreateAppointmentUseCase(IAppointmentRepository appointmentRepository)
        {
            this._appointmentRepository = appointmentRepository;
        }

        public ResponseAppointmentDto Run(CreateAppointmentDto dto)
        {
            var appointment = new Appointment(
                dto.VetId,
                dto.PetId,
                dto.DateAppointment
            );

            _appointmentRepository.Create(appointment);

            return new ResponseAppointmentDto
            {
                VetId = appointment.VetId,
                PetId = appointment.PetId,
                DateAppointment = appointment.DateAppointment
            };
        }
    }
}
