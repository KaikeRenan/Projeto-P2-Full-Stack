using Microsoft.EntityFrameworkCore;
using ProjetoP2.Infrastructure.Data.Context;
using ProjetoP2.Infrastructure.Data.Entities;
using ProjetoP2.Register.Domain.Entities;
using ProjetoP2.Register.Domain.IRepositories;
using ProjetoP2.Shared.Repositories;

namespace ProjetoP2.Register.Infrastructure.Repositories
{
    public class RegisterAppointmentRepository : BaseRepository<AppointmentRegister>, IAppointmentRegisterRepository
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
            var appointment = _appointments
                .FirstOrDefault(a => a.Id == id && a.RemovedAt == null);

            if (appointment == null) return null;

            return MapToDomain(appointment);
        }

        public override List<AppointmentRegister> GetAll()
        {
            return _appointments
                .Where(a => a.RemovedAt == null)
                .Select(a => MapToDomain(a))
                .ToList();
        }

        public override void Update(AppointmentRegister entity)
        {
            var appointment = _appointments
                .FirstOrDefault(a => a.Id == entity.Id && a.RemovedAt == null);

            if (appointment == null) return;

            appointment.Reschedule(entity.DateAppointment);

            _appointments.Update(appointment);
            _context.SaveChanges();
        }

        public override void Delete(AppointmentRegister entity)
        {
            var appointment = _appointments
                .FirstOrDefault(a => a.Id == entity.Id && a.RemovedAt == null);

            if (appointment == null) return;

            appointment.RemovedAt = DateTime.UtcNow;
            _appointments.Update(appointment);
            _context.SaveChanges();
        }

        private static AppointmentRegister MapToDomain(Appointment a)
        {
            return new AppointmentRegister(a.VetId, a.PetId, a.DateAppointment);
        }
    }
}
