using Microsoft.EntityFrameworkCore;
using ProjetoP2.Clinic.Domain.Entities;
using ProjetoP2.Clinic.Domain.IRepositories;
using ProjetoP2.Clinic.Infrastructure.Data;
using ProjetoP2.Infrastructure.Data.Context;
using ProjetoP2.Shared.Repositories;

namespace ProjetoP2.Clinic.Infrastructure.Repositories
{
    public class ClinicAppointmentRepository : BaseRepository<AppointmentClinic>, IAppointmentRepository
    {
        public ClinicAppointmentRepository(Context context) : base(context) { 

        }
    }
}
