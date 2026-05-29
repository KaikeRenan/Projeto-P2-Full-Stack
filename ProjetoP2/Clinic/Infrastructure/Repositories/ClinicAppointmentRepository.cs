using Microsoft.EntityFrameworkCore;
using ProjetoP2.Clinic.Domain.Entities;
using ProjetoP2.Clinic.Domain.IRepositories;
using ProjetoP2.Infrastructure.Data.Context;
using ProjetoP2.Infrastructure.Data.Entities;
using ProjetoP2.Shared.Repositories;

namespace ProjetoP2.Clinic.Infrastructure.Repositories
{
    public class ClinicAppointmentRepository : BaseRepository<AppointmentClinic>, IAppointmentRepository
    {
        private readonly DbSet<Appointment> _appointments;
        private readonly DbSet<VetClinic> _vets;

        public ClinicAppointmentRepository(Context context) : base(context)
        {
            _appointments = context.Set<Appointment>();
            _vets = context.Set<VetClinic>();
        }

        public override void Create(AppointmentClinic entity)
        {
            var appointment = new Appointment(entity.VetId, entity.PetId, entity.DateAppointment, entity.Notes);
            _appointments.Add(appointment);
            _context.SaveChanges();
        }

        public override AppointmentClinic? GetById(Guid id)
        {
            var appointment = _appointments
                .FirstOrDefault(a => a.Id == id && a.RemovedAt == null);

            if (appointment == null) return null;

            var vet = _vets
                .FirstOrDefault(v => v.Id == appointment.VetId && v.RemovedAt == null);

            return MapToDomain(appointment, vet);
        }

        public override List<AppointmentClinic> GetAll()
        {
            var appointments = _appointments
                .Where(a => a.RemovedAt == null)
                .ToList();

            var vetIds = appointments.Select(a => a.VetId).Distinct().ToList();
            var vets = _vets
                .Where(v => vetIds.Contains(v.Id) && v.RemovedAt == null)
                .ToDictionary(v => v.Id);

            return appointments
                .Select(a => MapToDomain(a, vets.GetValueOrDefault(a.VetId)))
                .ToList();
        }

        public override void Update(AppointmentClinic entity)
        {
            var appointment = _appointments
                .FirstOrDefault(a => a.Id == entity.Id && a.RemovedAt == null);

            if (appointment == null) return;

            appointment.ChangeVet(entity.VetId);
            appointment.Reschedule(entity.DateAppointment);
            appointment.UpdateNotes(entity.Notes);

            _appointments.Update(appointment);
            _context.SaveChanges();
        }

        public override void Delete(AppointmentClinic entity)
        {
            var appointment = _appointments
                .FirstOrDefault(a => a.Id == entity.Id && a.RemovedAt == null);

            if (appointment == null) return;

            appointment.RemovedAt = DateTime.UtcNow;
            _appointments.Update(appointment);
            _context.SaveChanges();
        }

        private static AppointmentClinic MapToDomain(Appointment a, VetClinic? vet)
        {
            var appt = new AppointmentClinic(a.VetId, a.PetId, a.DateAppointment);

            if (vet != null)
                appt.SetVet(vet);

            if (a.Notes != null)
                appt.UpdateNotes(a.Notes);

            return appt;
        }
    }
}
