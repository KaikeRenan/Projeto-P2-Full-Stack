using ProjetoP2.Register.Domain.Entities;
using ProjetoP2.Shared.IRepositories;

namespace ProjetoP2.Register.Domain.IRepositories
{
    public interface IAppointmentRegisterRepository : IBaseRepository<AppointmentRegister>
    {
        Task<bool> HasConflictAsync(Guid vetId, DateTime dateAppointment, Guid? appointmentId = null);
    }
}
