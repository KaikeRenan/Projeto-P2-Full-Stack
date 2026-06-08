using ProjetoP2.Clinic.Domain.IRepositories;
using ProjetoP2.Register.Domain.IRepositories;

namespace ProjetoP2.Register.Application.UseCases.PetUseCases
{
    public class DeletePetRegisterUseCase
    {
        private readonly IPetRegisterRepository _petRepository;
        private readonly IAppointmentRegisterRepository _appointmentRegisterRepository;
        private readonly IAppointmentClinicRepository _appointmentClinicRepository;

        public DeletePetRegisterUseCase(
            IPetRegisterRepository petRepository,
            IAppointmentRegisterRepository appointmentRegisterRepository,
            IAppointmentClinicRepository appointmentClinicRepository)
        {
            _petRepository = petRepository;
            _appointmentRegisterRepository = appointmentRegisterRepository;
            _appointmentClinicRepository = appointmentClinicRepository;
        }

        public async Task Run(Guid Id) 
        {
            var pet = await _petRepository.GetByIdAsync(Id);

            if (pet == null)
                throw new Exception("Pet não encontrado");

            // impedir exclusão de pets com consultas futuras

            var hasFutureAppointmentRegister = await _appointmentRegisterRepository.HasFutureAppointmentsAsync(Id);
            var hasFutureAppointmentClinic = await _appointmentClinicRepository.HasFutureAppointmentsAsync(Id);

            if (hasFutureAppointmentRegister || hasFutureAppointmentClinic)
                throw new Exception("Não é possível excluir um pet que possui consultas futuras");

            await _petRepository.DeleteAsync(pet);
        }
    }
}
