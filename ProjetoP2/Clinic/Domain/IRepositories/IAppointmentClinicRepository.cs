using ProjetoP2.Clinic.Domain.Entities;
using ProjetoP2.Shared.IRepositories;

namespace ProjetoP2.Clinic.Domain.IRepositories
{
    public interface IAppointmentClinicRepository : IBaseRepository<AppointmentClinic>
    {
        Task<bool> HasConflictAsync(Guid vetId, DateTime dateAppointment, Guid? appointmentId = null);
    }
}
