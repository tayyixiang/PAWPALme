using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PAWPALme.Models;

namespace PAWPALme.Data
{
    // CONTEXT: This class is the "Manager" for the database connection.
    // INHERITANCE: We inherit from 'IdentityDbContext' (not just DbContext).
    // This is crucial because it automatically adds all the security tables 
    // (AspNetUsers, AspNetRoles) needed for Login/Register to work.
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DBSETS: These properties tell Entity Framework: "Make these tables in SQL".
        // If you don't list a model here, it won't exist in the database.
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AdoptionApplication> AdoptionApplications { get; set; }



        // CONFIGURATION: This method runs exactly once when the app starts.
        // It is used to fine-tune how the database tables relate to each other.
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // IMPORTANT: Always call the base method first so Identity (Login) tables are set up correctly.
            base.OnModelCreating(builder);

            // FLUENT API: 
            // Here we manually define the rules for database relationships.
            // We do this to prevent "Cascading Delete Cycles" (a common SQL error).

            // 1. Appointment -> Pet Rule
            builder.Entity<Appointment>()
                .HasOne(a => a.Pet)            // An Appointment has one Pet
                .WithMany()                    // A Pet has many Appointments
                .HasForeignKey(a => a.PetId)   // Linked by PetId
                .OnDelete(DeleteBehavior.Restrict); // SAFETY RULE: If you try to delete a Pet that has an active Appointment, SQL will block it.

            // 2. Appointment -> Shelter Rule
            builder.Entity<Appointment>()
                .HasOne(a => a.Shelter)
                .WithMany()
                .HasForeignKey(a => a.ShelterId)
                .OnDelete(DeleteBehavior.Restrict); // SAFETY RULE: Prevents deleting a Shelter if they have upcoming bookings.

            // 3. Appointment -> Application Rule
            builder.Entity<Appointment>()
                .HasOne(a => a.AdoptionApplication)
                .WithMany()
                .HasForeignKey(a => a.AdoptionApplicationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}