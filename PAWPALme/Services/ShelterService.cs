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
            return await context.Shelter
                .AsNoTracking()
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        public async Task<Shelter?> GetShelterByIdAsync(int id)
        {
            using var context = _factory.CreateDbContext();
            return await context.Shelter.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Shelter?> GetShelterByOwnerUserIdAsync(string ownerUserId)
        {
            using var context = _factory.CreateDbContext();
            return await context.Shelter.AsNoTracking().FirstOrDefaultAsync(s => s.OwnerUserId == ownerUserId);
        }

        public async Task<Shelter> CreateShelterForOwnerAsync(Shelter shelter)
        {
            using var context = _factory.CreateDbContext();

            var exists = await context.Shelter.AnyAsync(s => s.OwnerUserId == shelter.OwnerUserId);
            if (exists)
                throw new InvalidOperationException("This user already has a shelter profile.");

            context.Shelter.Add(shelter);
            await context.SaveChangesAsync();
            return shelter;
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
            if (shelter is null) return;

            var hasPets = await context.Pet.AnyAsync(p => p.ShelterId == id);
            if (hasPets)
                throw new InvalidOperationException("Cannot delete a shelter that still has pets.");

            context.Shelter.Remove(shelter);
            await context.SaveChangesAsync();
        }
    }
}

