using Microsoft.JSInterop.Infrastructure;
using ProjetoP2.Clinic.Application.DTOs.Appointment;
using ProjetoP2.Clinic.Domain.Entities;
using ProjetoP2.Clinic.Domain.IRepositories;
using ProjetoP2.Shared.ValueObjects;

namespace ProjetoP2.Clinic.Application.UseCases.CreateAppointmentUseCase
{
    public class CreateAppointmentClinicUseCase
    {
        private readonly IAppointmentClinicRepository _appointmentRepository;

        public CreateAppointmentClinicUseCase(IAppointmentClinicRepository appointmentRepository)
        {
            this._appointmentRepository = appointmentRepository;
        }

        public ResponseAppointmentClinicDto Run(CreateAppointmentClinicDto dto)
        {
            var appointment = new AppointmentClinic(
                dto.VetId,
                dto.PetId,
                dto.DateAppointment
            );

            _appointmentRepository.Create(appointment);

            return new ResponseAppointmentClinicDto
            {
                VetId = appointment.VetId,
                PetId = appointment.PetId,
                DateAppointment = appointment.DateAppointment
            };
        }
    }
}
