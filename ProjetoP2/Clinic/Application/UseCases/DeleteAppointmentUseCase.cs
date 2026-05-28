
using ProjetoP2.Clinic.Domain.IRepositories;


namespace ProjetoP2.Clinic.Application.UseCases.DeleteAppointmentUseCase
{
    public class DeleteAppointmentUseCase
    {
        private readonly IAppointmentRepository _AppointmentRepository;

        public DeleteAppointmentUseCase(IAppointmentRepository AppointmentRepository)
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
