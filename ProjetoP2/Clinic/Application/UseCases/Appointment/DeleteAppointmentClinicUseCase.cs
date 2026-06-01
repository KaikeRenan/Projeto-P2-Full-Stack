using ProjetoP2.Clinic.Domain.IRepositories;


namespace ProjetoP2.Clinic.Application.UseCases.Appointment
{
    public class DeleteAppointmentClinicUseCase
    {
        private readonly IAppointmentClinicRepository _AppointmentRepository;

        public DeleteAppointmentClinicUseCase(IAppointmentClinicRepository AppointmentRepository)
        {
            _AppointmentRepository = AppointmentRepository;
        }

        public void Run(Guid Id) 
        {
            var owner = _AppointmentRepository.GetById(Id);

            if (owner == null)
                throw new Exception("Dono não foi encontrado");

            _AppointmentRepository.Delete(owner);
        }
    }
}
