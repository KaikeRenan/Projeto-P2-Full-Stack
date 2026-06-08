using ProjetoP2.Clinic.Application.DTOs.Appointment;
using ProjetoP2.Clinic.Domain.Entities;
using ProjetoP2.Clinic.Domain.IRepositories;
using ProjetoP2.Register.Domain.IRepositories;

namespace ProjetoP2.Clinic.Application.UseCases.Appointment
{
    public class CreateAppointmentClinicUseCase
    {
        private readonly IAppointmentClinicRepository _appointmentRepository;
        private readonly IVetClinicRepository _vetRepository;
        private readonly IPetRegisterRepository _petRepository;

        public CreateAppointmentClinicUseCase(
            IAppointmentClinicRepository appointmentRepository,
            IVetClinicRepository vetRepository,
            IPetRegisterRepository petRepository)
        {
            _appointmentRepository = appointmentRepository;
            _vetRepository = vetRepository;
            _petRepository = petRepository;
        }

        public async Task<ResponseAppointmentClinicDto> Run(CreateAppointmentClinicDto dto)
        {
            var vet = await _vetRepository.GetByIdAsync(dto.VetId);

            if (vet == null)
                throw new Exception("Veterinário não encontrado");

            var pet = await _petRepository.GetByIdAsync(dto.PetId);

            if (pet == null)
                throw new Exception("Pet não encontrado");

            // impedir consultas em horários conflitantes

            var conflict = await _appointmentRepository.HasConflictAsync(dto.VetId, dto.DateAppointment);

            if (conflict)
                throw new Exception("Já existe uma consulta agendada para este veterinário neste horário.");

            var appointment = new AppointmentClinic(
                dto.VetId,
                dto.PetId,
                dto.DateAppointment,
                dto.Notes
            );

            await _appointmentRepository.CreateAsync(appointment);

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
