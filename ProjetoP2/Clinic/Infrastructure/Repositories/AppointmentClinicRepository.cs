using Microsoft.EntityFrameworkCore;
using ProjetoP2.Clinic.Domain.Entities;
using ProjetoP2.Clinic.Domain.IRepositories;
using ProjetoP2.Infrastructure.Data.Context;
using ProjetoP2.Infrastructure.Data.Entities;
using ProjetoP2.Shared.Repositories;

namespace ProjetoP2.Clinic.Infrastructure.Repositories
{
    public class AppointmentClinicRepository : BaseRepository<AppointmentClinic>, IAppointmentClinicRepository
    {
        private readonly DbSet<Appointment> _appointments;
        private readonly DbSet<VetClinic> _vets;

        public AppointmentClinicRepository(Context context) : base(context)
        {
            _appointments = context.Set<Appointment>();
            _vets = context.Set<VetClinic>();
        }

        public override async Task CreateAsync(AppointmentClinic entity)
        {
            var appointment = new Appointment(entity.Id, entity.VetId, entity.PetId, entity.DateAppointment, entity.Notes);
            _appointments.Add(appointment);
            _dbContext.SaveChanges();
        }

        public override async Task<AppointmentClinic?> GetByIdAsync(Guid id)
        {
            var appointment = _appointments
                .FirstOrDefault(a => a.Id == id && a.RemovedAt == null);

            if (appointment == null) return null;

            var vet = _vets
                .FirstOrDefault(v => v.Id == appointment.VetId && v.RemovedAt == null);

            return MapToDomain(appointment, vet);
        }

        public override async Task<List<AppointmentClinic>> GetAllAsync()
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

        public override async Task UpdateAsync(AppointmentClinic entity)
        {
            var appointment = _appointments
                .FirstOrDefault(a => a.Id == entity.Id && a.RemovedAt == null);

            if (appointment == null) return;

            appointment.ChangeVet(entity.VetId);
            appointment.Reschedule(entity.DateAppointment);
            appointment.UpdateNotes(entity.Notes);

            _appointments.Update(appointment);
            _dbContext.SaveChanges();
        }

        public override async Task DeleteAsync(AppointmentClinic entity)
        {
            var appointment = _appointments
                .FirstOrDefault(a => a.Id == entity.Id && a.RemovedAt == null);

            if (appointment == null) return;

            appointment.RemovedAt = DateTime.UtcNow;
            _appointments.Update(appointment);
            _dbContext.SaveChanges();
        }

        private static AppointmentClinic MapToDomain(Appointment a, VetClinic? vet)
        {
            var appt = new AppointmentClinic(a.VetId, a.PetId, a.DateAppointment, a.Notes);

            appt.SetId(a.Id); // FORÇAR O ID DO BANCO PARA O DOMÍNIO -- EXPLICAR PARA O PROFESSOR

            if (vet != null)
                appt.SetVet(vet);

            if (a.Notes != null)
                appt.UpdateNotes(a.Notes);

            return appt;
        }

        // impedir consultas em horários conflitantes
        public async Task<bool> HasConflictAsync(Guid vetId, DateTime dateAppointment, Guid? appointmentId = null)
        {
            return await _appointments.AnyAsync(
                a => a.VetId == vetId && 
                a.DateAppointment == dateAppointment && 
                a.RemovedAt == null && 
                (appointmentId == null || a.Id != appointmentId));
        }

        // impedir exclusão de pets com consultas futuras
        public async Task<bool> HasFutureAppointmentsAsync(Guid petId)
        {
            return await _appointments.AnyAsync(
                a => a.PetId == petId &&
                a.DateAppointment > DateTime.UtcNow &&
                a.RemovedAt == null);
        }
    }
}
