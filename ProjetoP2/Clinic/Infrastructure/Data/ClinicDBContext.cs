using Microsoft.EntityFrameworkCore;
using ProjetoP2.Clinic.Domain.Entities;

namespace ProjetoP2.Clinic.Infrastructure.Data
{
    public class ClinicDBContext : DbContext
    {
        public DbSet<Vet> Vets => Set<Vet>();
        public DbSet<Appointment> Appointments => Set<Appointment>();

        public ClinicDBContext(DbContextOptions<ClinicDBContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("clinic");

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Vet)
                .WithMany(v => v.Appointments)
                .HasForeignKey(a => a.VetId);

            // FK para Pet (sem navegação de domínio!)
            modelBuilder.Entity<Appointment>()
                .Property(a => a.PetId)
                .IsRequired();
        }
    }
}
