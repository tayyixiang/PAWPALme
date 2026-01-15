using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PAWPALme.Models;

namespace PAWPALme.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pet> Pet { get; set; }
        public DbSet<Shelter> Shelter { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<AdoptionApplication> AdoptionApplication { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // --- FIX FOR MULTIPLE CASCADE PATHS ---

            // 1. When a Pet is deleted, DO NOT auto-delete Appointments (Prevent Cycle)
            builder.Entity<Appointment>()
                .HasOne(a => a.Pet)
                .WithMany()
                .HasForeignKey(a => a.PetId)
                .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.NoAction

            // 2. When a Shelter is deleted, DO NOT auto-delete Appointments (Prevent Cycle)
            builder.Entity<Appointment>()
                .HasOne(a => a.Shelter)
                .WithMany()
                .HasForeignKey(a => a.ShelterId)
                .OnDelete(DeleteBehavior.Restrict);

            // 3. When an Application is deleted, DO NOT auto-delete Appointments
            builder.Entity<Appointment>()
                .HasOne(a => a.AdoptionApplication)
                .WithMany()
                .HasForeignKey(a => a.AdoptionApplicationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}



