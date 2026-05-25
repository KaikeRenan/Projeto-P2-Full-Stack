using Microsoft.EntityFrameworkCore;
using ProjetoP2.Clinic.Domain.Entities;

namespace ProjetoP2.Clinic.Infrastructure.Data
{
    public class ClinicDBContext : DbContext
    {
        public DbSet<VetClinic> Vets => Set<VetClinic>();
        public DbSet<AppointmentClinic> Appointments => Set<AppointmentClinic>();

        public ClinicDBContext(DbContextOptions<ClinicDBContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("clinic");

            modelBuilder.Entity<AppointmentClinic>()
                .HasOne(a => a.Vet)
                .WithMany(v => v.Appointments)
                .HasForeignKey(a => a.VetId);

            // FK para Pet (sem navegação de domínio!)
            modelBuilder.Entity<AppointmentClinic>()
                .Property(a => a.PetId)
                .IsRequired();
        }
    }
}
