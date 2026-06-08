using ProjetoP2.Clinic.Domain.IRepositories;
using ProjetoP2.Register.Application.DTOs.AppointmentRegister;
using ProjetoP2.Register.Domain.Entities;
using ProjetoP2.Register.Domain.IRepositories;

namespace ProjetoP2.Register.Application.UseCases.AppointmentRegisterUseCases
{
    public class CreateAppointmentRegisterUseCase
    {
        private readonly IAppointmentRegisterRepository _appointmentRepository;
        private readonly IVetClinicRepository _vetRepository;
        private readonly IPetRegisterRepository _petRepository;

        public CreateAppointmentRegisterUseCase(
            IAppointmentRegisterRepository appointmentRepository,
            IVetClinicRepository vetRepository,
            IPetRegisterRepository petRepository)
        {
            _appointmentRepository = appointmentRepository;
            _vetRepository = vetRepository;
            _petRepository = petRepository;
        }

        public async Task<ResponseAppointmentRegisterDto> Run(CreateAppointmentRegisterDto dto)
        {
            var vet = await _vetRepository.GetByIdAsync(dto.VetId);

            if (vet == null)
                throw new Exception("Veterinário não encontrado");

            var pet = await _petRepository.GetByIdAsync(dto.PetId);

            if (pet == null)
                throw new Exception("Pet não encontrado");

            var conflict = await _appointmentRepository.HasConflictAsync(dto.VetId, dto.DateAppointment);

            if (conflict)
                throw new Exception("Já existe uma consulta agendada para este veterinário neste horário.");

            var appointment = new AppointmentRegister(
                dto.VetId,
                dto.PetId,
                dto.DateAppointment
            );

            await _appointmentRepository.CreateAsync(appointment);

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
