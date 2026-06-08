using ProjetoP2.Register.Domain.IRepositories;

namespace ProjetoP2.Register.Application.UseCases.AppointmentRegisterUseCases
{
    public class DeleteAppointmentRegisterUseCase
    {
        private readonly IAppointmentRegisterRepository _appointmentRepository;

        public DeleteAppointmentRegisterUseCase(IAppointmentRegisterRepository appointmentRepository)
        {
            this._appointmentRepository = appointmentRepository;
        }

        public async Task Run(Guid Id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(Id);

            if (appointment == null)
                throw new Exception("Consulta não encontrada");

            await _appointmentRepository.DeleteAsync(appointment);
        }
    }
}
