using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PAWPALme.Enums;
using PAWPALme.Models;

namespace PAWPALme.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // 1. Ensure Database is Created and Migrations Applied
            await context.Database.MigrateAsync();

            // 2. Ensure Roles Exist (Safety check, even if Program.cs does it)
            string[] roles = ["Admin", "Shelter", "Adopter"];
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // 3. Seed Users (So the teacher can login immediately)
            var shelterUser = await SeedUserAsync(userManager, "teacher@shelter.com", "Shelter", "Password123!");
            var adopterUser = await SeedUserAsync(userManager, "teacher@adopter.com", "Adopter", "Password123!");
            var adminUser = await SeedUserAsync(userManager, "admin@pawpal.com", "Admin", "Password123!");

            // 4. Seed Shelters (Owned by Yixiang)
            if (!context.Shelter.Any())
            {
                var shelters = new List<Shelter>
                {
                    new Shelter
                    {
                        Name = "Sunny Side Rescue",
                        Address = "123 Clementi Road, Singapore 120123",
                        Phone = "91234567",
                        Description = "A safe haven for stray dogs and cats in the West.",
                        OwnerUserId = shelterUser.Id, // Linked to the teacher account
                        ImageUrl = "https://placehold.co/400x300?text=Sunny+Side" // Placeholder image
                    },
                    new Shelter
                    {
                        Name = "Paws & Claws East",
                        Address = "88 Pasir Ris Dr, Singapore 510088",
                        Phone = "67891234",
                        Description = "Specializing in rehabilitation of senior pets.",
                        OwnerUserId = null, // Orphaned shelter (for testing)
                        ImageUrl = "https://placehold.co/400x300?text=Paws+Claws"
                    }
                };
                context.Shelter.AddRange(shelters);
                await context.SaveChangesAsync();
            }

            // 5. Seed Pets (Owned by Yixiang)
            if (!context.Pet.Any())
            {
                var shelter1 = await context.Shelter.FirstOrDefaultAsync(s => s.Name == "Sunny Side Rescue");
                var shelter2 = await context.Shelter.FirstOrDefaultAsync(s => s.Name == "Paws & Claws East");

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
                        ShelterId = shelter1!.Id,
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
                        Status = PetStatus.Pending, // Pending adoption
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
                context.Pet.AddRange(pets);
                await context.SaveChangesAsync();
            }

            // 6. Seed Appointments (Owned by Yixiang)
            if (!context.Appointment.Any())
            {
                var pet = await context.Pet.FirstOrDefaultAsync(p => p.Name == "Luna");
                var shelter = await context.Shelter.FindAsync(pet!.ShelterId);

                var appointments = new List<Appointment>
                {
                    new Appointment
                    {
                        AppointmentDate = DateTime.Today.AddDays(3),
                        AppointmentTime = new TimeSpan(14, 30, 0), // 2:30 PM
                        Status = AppointmentStatus.Pending,
                        Notes = "I am looking for a cat that gets along with others.",
                        ShelterRemarks = null,
                        PetId = pet.Id,
                        ShelterId = shelter!.Id,
                        AdopterUserId = adopterUser.Id // Linked to teacher adopter account
                    },
                    new Appointment
                    {
                        AppointmentDate = DateTime.Today.AddDays(-2), // Past appointment
                        AppointmentTime = new TimeSpan(10, 0, 0),
                        Status = AppointmentStatus.Completed,
                        Notes = "First visit.",
                        ShelterRemarks = "Adopter didn't show up.",
                        PetId = pet.Id,
                        ShelterId = shelter.Id,
                        AdopterUserId = adopterUser.Id
                    }
                };
                context.Appointment.AddRange(appointments);
                await context.SaveChangesAsync();
            }
        }

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