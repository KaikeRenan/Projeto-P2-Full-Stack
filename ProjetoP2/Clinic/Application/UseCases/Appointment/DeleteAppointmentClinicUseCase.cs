using ProjetoP2.Clinic.Domain.IRepositories;

namespace ProjetoP2.Clinic.Application.UseCases.Appointment
{
    public class DeleteAppointmentClinicUseCase
    {
        private readonly IAppointmentClinicRepository _appointmentRepository;

        public DeleteAppointmentClinicUseCase(IAppointmentClinicRepository appointmentRepository)
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
