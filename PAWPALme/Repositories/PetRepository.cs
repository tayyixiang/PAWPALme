using Microsoft.EntityFrameworkCore;
using PAWPALme.Data;
using PAWPALme.Models;

namespace PAWPALme.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public PetRepository(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<Pet>> GetAllAsync()
        {
            using var context = _factory.CreateDbContext();
            return await context.Pet
                .Include(p => p.Shelter)
                .ToListAsync();
        }

        public async Task<Pet?> GetByIdAsync(int id)
        {
            using var context = _factory.CreateDbContext();
            return await context.Pet
                .Include(p => p.Shelter)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // --- THE MISSING METHOD IMPLEMENTATION ---
        public async Task<IEnumerable<Pet>> GetPetsByShelterIdAsync(int shelterId)
        {
            using var context = _factory.CreateDbContext();
            return await context.Pet
                .Where(p => p.ShelterId == shelterId)
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }

        public async Task AddAsync(Pet pet)
        {
            using var context = _factory.CreateDbContext();
            context.Pet.Add(pet);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Pet pet)
        {
            using var context = _factory.CreateDbContext();
            context.Pet.Update(pet);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _factory.CreateDbContext();
            var pet = await context.Pet.FindAsync(id);
            if (pet != null)
            {
                context.Pet.Remove(pet);
                await context.SaveChangesAsync();
            }
        }
    }
}