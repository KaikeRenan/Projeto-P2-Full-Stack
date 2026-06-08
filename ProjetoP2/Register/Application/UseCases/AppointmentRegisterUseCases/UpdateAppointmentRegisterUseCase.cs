using ProjetoP2.Clinic.Domain.IRepositories;
using ProjetoP2.Register.Application.DTOs.AppointmentRegister;
using ProjetoP2.Register.Domain.IRepositories;

namespace ProjetoP2.Register.Application.UseCases.AppointmentRegisterUseCases
{
    public class UpdateAppointmentRegisterUseCase
    {
        private readonly IAppointmentRegisterRepository _appointmentRepository;
        private readonly IVetClinicRepository _vetRepository;
        private readonly IPetRegisterRepository _petRepository;

        public UpdateAppointmentRegisterUseCase(
            IAppointmentRegisterRepository appointmentRepository,
            IVetClinicRepository vetRepository,
            IPetRegisterRepository petRepository)
        {
            _appointmentRepository = appointmentRepository;
            _vetRepository = vetRepository;
            _petRepository = petRepository;
        }

        public async Task<ResponseAppointmentRegisterDto> Run(UpdateAppointmentRegisterDto dto)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(dto.Id);

            if (appointment == null)
                throw new Exception("Consulta não encontrada");

            var vet = await _vetRepository.GetByIdAsync(dto.VetId);

            if (vet == null)
                throw new Exception("Veterinário não encontrado");

            var pet = await _petRepository.GetByIdAsync(dto.PetId);

            if (pet == null)
                throw new Exception("Pet não encontrado");

            // impedir consultas em horários conflitantes

            var conflict = await _appointmentRepository.HasConflictAsync(dto.VetId, dto.DateAppointment, dto.Id);

            if (conflict)
                throw new Exception("Já existe uma consulta agendada para este veterinário neste horário.");

            appointment.Update(
                dto.VetId,
                dto.PetId,
                dto.DateAppointment
            );

            await _appointmentRepository.UpdateAsync(appointment);

            return new ResponseAppointmentRegisterDto
            {
                Id = appointment.Id,
                VetId = appointment.VetId,
                PetId = appointment.PetId,
                DateAppointment = appointment.DateAppointment,
            };
        }
    }
}
