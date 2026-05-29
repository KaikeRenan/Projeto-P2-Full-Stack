using Microsoft.EntityFrameworkCore;
using ProjetoP2.Clinic.Domain.Entities;

namespace ProjetoP2.Clinic.Infrastructure.Data
{
    public class ClinicDBContext : DbContext
    {
        public DbSet<VetClinic> VetClinics { get; set; } = null!;
        public DbSet<AppointmentClinic> AppointmentClinics { get; set; } = null!;

        public ClinicDBContext(DbContextOptions<ClinicDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<VetClinic>(vet =>
            {
                vet.ToTable("VetClinics");
                vet.HasKey(v => v.Id);

                vet.Property(v => v.FirstName)
                   .IsRequired()
                   .HasMaxLength(100);

                vet.Property(v => v.LastName)
                   .IsRequired()
                   .HasMaxLength(100);

                
                vet.OwnsOne(v => v.Email, email =>
                {
                    email.Property(e => e.Value)
                         .HasColumnName("Email")
                         .IsRequired()
                         .HasMaxLength(254);
                    email.HasIndex(e => e.Value).IsUnique();
                });

               
                vet.OwnsOne(v => v.PhoneNumber, phone =>
                {
                    phone.Property(p => p.Value)
                         .HasColumnName("PhoneNumber")
                         .IsRequired()
                         .HasMaxLength(20);
                });

                
                vet.OwnsOne(v => v.CPF, cpf =>
                {
                    cpf.Property(c => c.Value)
                       .HasColumnName("CPF")
                       .IsRequired()
                       .HasMaxLength(11);
                    cpf.HasIndex(c => c.Value).IsUnique();
                });

                vet.OwnsOne(v => v.CRMV, crmv =>
                {
                    crmv.Property(c => c.Value)
                        .HasColumnName("CRMV")
                        .IsRequired()
                        .HasMaxLength(20);
                    crmv.HasIndex(c => c.Value).IsUnique();
                });

             
                vet.HasQueryFilter(v => v.RemovedAt == null);

                vet.HasMany(v => v.Appointments)
                   .WithOne(a => a.Vet)
                   .HasForeignKey(a => a.VetId)
                   .OnDelete(DeleteBehavior.Restrict);
            });

            
            modelBuilder.Entity<AppointmentClinic>(appt =>
            {
                appt.ToTable("AppointmentClinics");
                appt.HasKey(a => a.Id);

                appt.Property(a => a.VetId).IsRequired();
                appt.Property(a => a.PetId).IsRequired();

                appt.Property(a => a.DateAppointment)
                    .IsRequired();

                appt.Property(a => a.Notes)
                    .HasMaxLength(1000);

                appt.HasQueryFilter(a => a.RemovedAt == null);
            });
        }
    };
};