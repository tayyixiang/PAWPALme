using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PAWPALme.Enums;
using PAWPALme.Models;

namespace PAWPALme.Data
{
    // ARCHITECTURE: Static Data Seeder
    // Executed during application startup to ensure the database starts in a known, valid state.
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            // SERVICE SCOPE: Create a temporary scope to resolve scoped services (DbContext, Managers)
            // ensuring resources are disposed of immediately after seeding.
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // 1. MIGRATION: Applies any pending EF Core migrations to update the SQL schema
            await context.Database.MigrateAsync();

            // 2. RBAC SETUP: Idempotent check to ensure system Roles exist
            string[] roles = ["Admin", "Shelter", "Adopter"];
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // 3. IDENTITY SEEDING: Creating default accounts for development/testing
            var shelterUser = await SeedUserAsync(userManager, "teacher@shelter.com", "Shelter", "Password123!");
            var adopterUser = await SeedUserAsync(userManager, "teacher@adopter.com", "Adopter", "Password123!");
            var adminUser = await SeedUserAsync(userManager, "admin@pawpal.com", "Admin", "Password123!");

            // 4. ENTITY SEEDING: Checks if data exists (.Any()) before inserting to prevent duplicates
            if (!context.Shelters.Any()) // FIX: Plural 'Shelters'
            {
                var shelters = new List<Shelter>
                {
                    new Shelter
                    {
                        Name = "Sunny Side Rescue",
                        Address = "123 Clementi Road, Singapore 120123",
                        Phone = "91234567",
                        Description = "A safe haven for stray dogs and cats in the West.",
                        OwnerUserId = shelterUser.Id, // RELATIONAL: Links to Identity User
                        ImageUrl = "https://placehold.co/400x300?text=Sunny+Side"
                    },
                    new Shelter
                    {
                        Name = "Paws & Claws East",
                        Address = "88 Pasir Ris Dr, Singapore 510088",
                        Phone = "67891234",
                        Description = "Specializing in rehabilitation of senior pets.",
                        OwnerUserId = null,
                        ImageUrl = "https://placehold.co/400x300?text=Paws+Claws"
                    }
                };
                context.Shelters.AddRange(shelters);
                await context.SaveChangesAsync(); // COMMIT: Transaction commits here
            }

            // 5. DEPENDENT DATA: Pets (Requires Shelters to exist first)
            if (!context.Pets.Any()) // FIX: Plural 'Pets'
            {
                // QUERY: Retrieving Parent IDs to maintain Referential Integrity
                var shelter1 = await context.Shelters.FirstOrDefaultAsync(s => s.Name == "Sunny Side Rescue");
                var shelter2 = await context.Shelters.FirstOrDefaultAsync(s => s.Name == "Paws & Claws East");

                var pets = new List<Pet>
                {
                    new Pet
                    {
                        Name = "Max",
                        Species = "Dog",
                        Breed = "Golden Retriever",
                        Age = 3,
                        Size = "Large",
                        Gender = PetGender.Male,
                        Status = PetStatus.Available,
                        Description = "Friendly and loves to play fetch. Great with kids.",
                        ShelterId = shelter1!.Id, // FK ASSIGNMENT
                        ImageUrl = "https://placehold.co/400x400?text=Max"
                    },
                    new Pet
                    {
                        Name = "Luna",
                        Species = "Cat",
                        Breed = "Siamese",
                        Age = 2,
                        Size = "Small",
                        Gender = PetGender.Female,
                        Status = PetStatus.Pending,
                        Description = "Quiet and affectionate. Prefers a calm household.",
                        ShelterId = shelter1.Id,
                        ImageUrl = "https://placehold.co/400x400?text=Luna"
                    },
                    new Pet
                    {
                        Name = "Rocky",
                        Species = "Dog",
                        Breed = "Bulldog",
                        Age = 5,
                        Size = "Medium",
                        Gender = PetGender.Male,
                        Status = PetStatus.Available,
                        Description = "Lazy but lovable. Loves belly rubs.",
                        ShelterId = shelter2!.Id,
                        ImageUrl = "https://placehold.co/400x400?text=Rocky"
                    }
                };
                context.Pets.AddRange(pets);
                await context.SaveChangesAsync();
            }

            // 6. TRANSACTIONAL DATA: Appointments (Requires Pets, Shelters, and Users)
            if (!context.Appointments.Any()) // FIX: Plural 'Appointments'
            {
                var pet = await context.Pets.FirstOrDefaultAsync(p => p.Name == "Luna");
                var shelter = await context.Shelters.FindAsync(pet!.ShelterId);

                var appointments = new List<Appointment>
                {
                    new Appointment
                    {
                        AppointmentDate = DateTime.Today.AddDays(3),
                        AppointmentTime = new TimeSpan(14, 30, 0),
                        Status = AppointmentStatus.Pending,
                        Notes = "I am looking for a cat that gets along with others.",
                        ShelterRemarks = null,
                        PetId = pet.Id,
                        ShelterId = shelter!.Id,
                        AdopterUserId = adopterUser.Id
                    },
                    new Appointment
                    {
                        AppointmentDate = DateTime.Today.AddDays(-2),
                        AppointmentTime = new TimeSpan(10, 0, 0),
                        Status = AppointmentStatus.Completed,
                        Notes = "First visit.",
                        ShelterRemarks = "Adopter didn't show up.",
                        PetId = pet.Id,
                        ShelterId = shelter.Id,
                        AdopterUserId = adopterUser.Id
                    }
                };
                context.Appointments.AddRange(appointments);
                await context.SaveChangesAsync();
            }
        }

        // HELPER: Reusable logic to ensure user existence
        private static async Task<ApplicationUser> SeedUserAsync(UserManager<ApplicationUser> userManager, string email, string role, string password)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
            return user;
        }
    }
}