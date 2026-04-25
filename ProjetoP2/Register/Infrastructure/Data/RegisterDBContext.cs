using Microsoft.EntityFrameworkCore;
using ProjetoP2.Register.Domain.Entities;

namespace ProjetoP2.Register.Infrastructure.Data
{
    public class RegisterDBContext : DbContext
    {
        public DbSet<Owner> Owners => Set<Owner>();
        public DbSet<Pet> Pets => Set<Pet>();

        public RegisterDBContext(DbContextOptions<RegisterDBContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("register");

            modelBuilder.Entity<Owner>()
                .HasMany(o => o.Pets)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId);

            modelBuilder.Entity<Owner>().OwnsOne(o => o.Email);
            modelBuilder.Entity<Owner>().OwnsOne(o => o.PhoneNumber);
            modelBuilder.Entity<Owner>().OwnsOne(o => o.CPF);
        }
    }
}
