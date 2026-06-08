using Microsoft.EntityFrameworkCore;
using ProjetoP2.Infrastructure.Data.Context;
using ProjetoP2.Infrastructure.Data.Entities;
using ProjetoP2.Register.Domain.Entities;
using ProjetoP2.Register.Domain.IRepositories;
using ProjetoP2.Shared.Repositories;

namespace ProjetoP2.Register.Infrastructure.Repositories
{
    public class AppointmentRegisterRepository : BaseRepository<AppointmentRegister>, IAppointmentRegisterRepository
    {
        private readonly DbSet<Appointment> _appointments;

        public AppointmentRegisterRepository(Context context) : base(context)
        {
            _appointments = context.Set<Appointment>();
        }

        public override async Task CreateAsync(AppointmentRegister entity)
        {
            var appointment = new Appointment(entity.Id, entity.VetId, entity.PetId, entity.DateAppointment);
            _appointments.Add(appointment);
            _dbContext.SaveChanges();
        }

        public override async Task<AppointmentRegister?> GetByIdAsync(Guid id)
        {
            var appointment = _appointments
                .FirstOrDefault(a => a.Id == id && a.RemovedAt == null);

            if (appointment == null) return null;

            return MapToDomain(appointment);
        }

        public override async Task<List<AppointmentRegister>> GetAllAsync()
        {
            return _appointments
                .Where(a => a.RemovedAt == null)
                .Select(a => MapToDomain(a))
                .ToList();
        }

        public override async Task UpdateAsync(AppointmentRegister entity)
        {
            var appointment = _appointments
                .FirstOrDefault(a => a.Id == entity.Id && a.RemovedAt == null);

            if (appointment == null) return;

            appointment.Reschedule(entity.DateAppointment);

            _appointments.Update(appointment);
            _dbContext.SaveChanges();
        }

        public override async Task DeleteAsync(AppointmentRegister entity)
        {
            var appointment = _appointments
                .FirstOrDefault(a => a.Id == entity.Id && a.RemovedAt == null);

            if (appointment == null) return;

            appointment.RemovedAt = DateTime.UtcNow;
            _appointments.Update(appointment);
            _dbContext.SaveChanges();
        }

        private static AppointmentRegister MapToDomain(Appointment a)
        {
            var appt = new AppointmentRegister(a.VetId, a.PetId, a.DateAppointment);

            appt.SetId(a.Id); // FORÇAR O ID DO BANCO PARA O DOMÍNIO -- EXPLICAR PARA O PROFESSOR

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
