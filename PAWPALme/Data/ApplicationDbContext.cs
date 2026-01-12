using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PAWPALme.Models;

namespace PAWPALme.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Pet> Pet { get; set; } = default!;
        public DbSet<Shelter> Shelter { get; set; } = default!;
        public DbSet<Appointment> Appointment { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Shelter>()
                .HasIndex(s => s.OwnerUserId)
                .IsUnique();

            builder.Entity<Pet>()
                .HasOne<Shelter>()
                .WithMany()
                .HasForeignKey(p => p.ShelterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

