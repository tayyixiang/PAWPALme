using Microsoft.EntityFrameworkCore;
using PAWPALme.Data;
using PAWPALme.Models;

namespace PAWPALme.Services
{
    public class ShelterService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public ShelterService(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<Shelter>> GetSheltersAsync()
        {
            using var context = _factory.CreateDbContext();
            return await context.Shelter.OrderBy(s => s.Name).ToListAsync();
        }

        public async Task<Shelter?> GetShelterByIdAsync(int id)
        {
            using var context = _factory.CreateDbContext();
            return await context.Shelter.FindAsync(id);
        }

        public async Task<Shelter?> GetShelterByOwnerUserIdAsync(string ownerUserId)
        {
            using var context = _factory.CreateDbContext();
            return await context.Shelter.FirstOrDefaultAsync(s => s.OwnerUserId == ownerUserId);
        }

        public async Task<int> CreateOrUpdateShelterForOwnerAsync(
            string ownerUserId,
            string name,
            string? address,
            string? phone,
            string? description)
        {
            using var context = _factory.CreateDbContext();

            var shelter = await context.Shelter.FirstOrDefaultAsync(s => s.OwnerUserId == ownerUserId);

            if (shelter == null)
            {
                shelter = new Shelter
                {
                    OwnerUserId = ownerUserId,
                    Name = name,
                    Address = address,
                    Phone = phone,
                    Description = description
                };
                context.Shelter.Add(shelter);
            }
            else
            {
                shelter.Name = name;
                shelter.Address = address;
                shelter.Phone = phone;
                shelter.Description = description;
                context.Shelter.Update(shelter);
            }

            await context.SaveChangesAsync();
            return shelter.Id;
        }

        public async Task AddShelterAsync(Shelter shelter)
        {
            using var context = _factory.CreateDbContext();
            context.Shelter.Add(shelter);
            await context.SaveChangesAsync();
        }

        public async Task UpdateShelterAsync(Shelter shelter)
        {
            using var context = _factory.CreateDbContext();
            context.Shelter.Update(shelter);
            await context.SaveChangesAsync();
        }

        public async Task DeleteShelterAsync(int id)
        {
            using var context = _factory.CreateDbContext();
            var shelter = await context.Shelter.FindAsync(id);
            var hasPets = await context.Pet.AnyAsync(p => p.ShelterId == id);

            if (shelter != null && !hasPets)
            {
                context.Shelter.Remove(shelter);
                await context.SaveChangesAsync();
            }
        }
    }
}

