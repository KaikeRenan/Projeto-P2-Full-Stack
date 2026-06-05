using Microsoft.EntityFrameworkCore;
using ProjetoP2.Clinic.Domain.Entities;
using ProjetoP2.Register.Domain.Entities;

namespace ProjetoP2.Infrastructure.Data.Context
{
    public class Context:DbContext
    {
        public DbSet<OwnerRegister> OwnerRegisters { get; set; } = null!;
        public DbSet<PetRegister> PetRegisters { get; set; } = null!;
        public DbSet<AppointmentRegister> AppointmentRegisters { get; set; } = null!;
        public DbSet<VetClinic> VetClinics { get; set; } = null!;
        public DbSet<AppointmentClinic> AppointmentClinics { get; set; } = null!;

        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OwnerRegister>(owner =>
            {
                owner.ToTable("OwnerRegisters");
                owner.HasKey(o => o.Id);

                owner.Property(o => o.FirstName)
                     .IsRequired()
                     .HasMaxLength(100);

                owner.Property(o => o.LastName)
                     .IsRequired()
                     .HasMaxLength(100);

                // Email
                owner.OwnsOne(o => o.Email, email =>
                {
                    email.Property(e => e.Value)
                         .HasColumnName("Email")
                         .IsRequired()
                         .HasMaxLength(254);
                    email.HasIndex(e => e.Value).IsUnique();
                });

                // PhoneNumber
                owner.OwnsOne(o => o.PhoneNumber, phone =>
                {
                    phone.Property(p => p.Value)
                         .HasColumnName("PhoneNumber")
                         .IsRequired()
                         .HasMaxLength(20);
                });

                // CPF
                owner.OwnsOne(o => o.CPF, cpf =>
                {
                    cpf.Property(c => c.Value)
                       .HasColumnName("CPF")
                       .IsRequired()
                       .HasMaxLength(11);
                    cpf.HasIndex(c => c.Value).IsUnique();
                });

                owner.HasQueryFilter(o => o.RemovedAt == null);

                // ── Relacionamento 1:N → PetRegister 
                owner.HasMany(o => o.Pets)
                     .WithOne(p => p.Owner)
                     .HasForeignKey(p => p.OwnerId)
                     .OnDelete(DeleteBehavior.Restrict);
            });

            // ── PetRegister 
            modelBuilder.Entity<PetRegister>(pet =>
            {
                pet.ToTable("PetRegisters");
                pet.HasKey(p => p.Id);

                pet.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(100);

                pet.OwnsOne(p => p.Color, color =>
                {
                    color.Property(c => c.Value)
                         .HasColumnName("Color")
                         .HasMaxLength(50);
                });

                pet.OwnsOne(p => p.Specie, specie =>
                {
                    specie.Property(s => s.Value)
                          .HasColumnName("Specie")
                          .HasMaxLength(50);
                });

                pet.OwnsOne(p => p.Sex, sex =>
                {
                    sex.Property(s => s.Value)
                       .HasColumnName("Sex")
                       .HasMaxLength(20);
                });

                pet.Property(p => p.Castrated).IsRequired();
                pet.Property(p => p.Community).IsRequired();
                pet.Property(p => p.Microchipped).IsRequired();

                pet.Property(p => p.MicrochippedNumber);

                pet.Property(p => p.BirthDate).IsRequired();

                pet.Property(p => p.PhotoURL).HasMaxLength(2048);

                pet.Property(p => p.OwnerId);

                // State e City (nullable Value Objects)
                pet.OwnsOne(p => p.State, state =>
                {
                    state.Property(s => s.Value)
                         .HasColumnName("State")
                         .HasMaxLength(2);
                });

                pet.OwnsOne(p => p.City, city =>
                {
                    city.Property(c => c.Value)
                        .HasColumnName("City")
                        .HasMaxLength(100);
                });

                // PetRG (nullable Value Object)
                pet.OwnsOne(p => p.PetRG, rg =>
                {
                    rg.Property(r => r.Value)
                      .HasColumnName("PetRG")
                      .HasMaxLength(20);
                });

                pet.HasQueryFilter(p => p.RemovedAt == null);
            });

            // ── AppointmentRegister 
            modelBuilder.Entity<AppointmentRegister>(appt =>
            {
                appt.ToTable("AppointmentRegisters");
                appt.HasKey(a => a.Id);

                appt.Property(a => a.PetId).IsRequired();
                appt.Property(a => a.DateAppointment).IsRequired();


                appt.HasQueryFilter(a => a.RemovedAt == null);
            });
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
    }
}
