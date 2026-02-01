using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PAWPALme.Models;

namespace PAWPALme.Data
{
    // CONTEXT: Main entry point for Entity Framework to interact with SQL
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DBSETS: These map C# classes to SQL tables.
        // Naming Convention: Pluralized (Pets, Shelters) to represent collections of records.
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AdoptionApplication> AdoptionApplications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // FLUENT API: Explicitly defining complex relationships and cascade behaviors
            // to prevent "Multiple Cascade Paths" SQL errors.

            // 1. Appointment -> Pet Relationship
            builder.Entity<Appointment>()
                .HasOne(a => a.Pet)
                .WithMany()
                .HasForeignKey(a => a.PetId)
                .OnDelete(DeleteBehavior.Restrict); // RESTRICT: Deleting a Pet fails if Appointments exist

            // 2. Appointment -> Shelter Relationship
            builder.Entity<Appointment>()
                .HasOne(a => a.Shelter)
                .WithMany()
                .HasForeignKey(a => a.ShelterId)
                .OnDelete(DeleteBehavior.Restrict); // RESTRICT: Prevents orphan appointments

            // 3. Appointment -> AdoptionApplication Relationship
            builder.Entity<Appointment>()
                .HasOne(a => a.AdoptionApplication)
                .WithMany()
                .HasForeignKey(a => a.AdoptionApplicationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}