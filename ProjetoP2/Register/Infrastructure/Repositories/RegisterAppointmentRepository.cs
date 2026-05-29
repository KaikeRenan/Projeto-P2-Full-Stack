using Microsoft.EntityFrameworkCore;
using ProjetoP2.Infrastructure.Data.Context;
using ProjetoP2.Infrastructure.Data.Entities;
using ProjetoP2.Register.Domain.Entities;
using ProjetoP2.Register.Domain.IRepositories;
using ProjetoP2.Shared.Repositories;

namespace ProjetoP2.Register.Infrastructure.Repositories
{
    public class RegisterAppointmentRepository : BaseRepository<AppointmentRegister>, IAppointmentRepository
    {
        private readonly DbSet<Appointment> _appointments;

        public RegisterAppointmentRepository(Context context) : base(context)
        {
            _appointments = context.Set<Appointment>();
        }

        public override void Create(AppointmentRegister entity)
        {
            var appointment = new Appointment(entity.VetId, entity.PetId, entity.DateAppointment);
            _appointments.Add(appointment);
            _context.SaveChanges();
        }

        public override AppointmentRegister? GetById(Guid id)
        {
            return _appointments
                .Where(a => a.Id == id && a.RemovedAt == null)
                .Select(a => new AppointmentRegister(a.VetId, a.PetId, a.DateAppointment))
                .FirstOrDefault();
        }

        public override List<AppointmentRegister> GetAll()
        {
            return _appointments
                .Where(a => a.RemovedAt == null)
                .Select(a => new AppointmentRegister(a.VetId, a.PetId, a.DateAppointment))
                .ToList();
        }
    }
}
