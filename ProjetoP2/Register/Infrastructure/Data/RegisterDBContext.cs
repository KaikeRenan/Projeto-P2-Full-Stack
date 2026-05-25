using Microsoft.EntityFrameworkCore;
using ProjetoP2.Register.Domain.Entities;

namespace ProjetoP2.Register.Infrastructure.Data
{
    public class RegisterDBContext : DbContext
    {
        public DbSet<OwnerRegister> Owners => Set<OwnerRegister>();
        public DbSet<PetRegister> Pets => Set<PetRegister>();

        public RegisterDBContext(DbContextOptions<RegisterDBContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("register");

            modelBuilder.Entity<OwnerRegister>()
                .HasMany(o => o.Pets)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId);

            modelBuilder.Entity<OwnerRegister>().OwnsOne(o => o.Email);
            modelBuilder.Entity<OwnerRegister>().OwnsOne(o => o.PhoneNumber);
            modelBuilder.Entity<OwnerRegister>().OwnsOne(o => o.CPF);
        }
    }
}
