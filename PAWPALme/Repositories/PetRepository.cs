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

        public async Task<Pet?> GetByIdAsync(int id)
        {
            using var context = _factory.CreateDbContext();
            return await context.Pet.Include(p => p.Shelter).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Pet>> GetAllAsync()
        {
            using var context = _factory.CreateDbContext();
            return await context.Pet.Include(p => p.Shelter).ToListAsync();
        }

        public async Task<IEnumerable<Pet>> GetPetsByShelterIdAsync(int shelterId)
        {
            using var context = _factory.CreateDbContext();
            return await context.Pet.Where(p => p.ShelterId == shelterId).ToListAsync();
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

        // --- NEW SEARCH LOGIC ---
        public async Task<IEnumerable<Pet>> SearchAsync(string? searchTerm, string? species, string? breed, int? age)
        {
            using var context = _factory.CreateDbContext();
            var query = context.Pet.Include(p => p.Shelter).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string term = searchTerm.ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(term) || p.Description.ToLower().Contains(term));
            }

            if (!string.IsNullOrWhiteSpace(species) && species != "All")
            {
                query = query.Where(p => p.Species == species);
            }

            if (!string.IsNullOrWhiteSpace(breed) && breed != "All")
            {
                query = query.Where(p => p.Breed == breed);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<string>> GetBreedsAsync()
        {
            using var context = _factory.CreateDbContext();
            return await context.Pet.Select(p => p.Breed).Distinct().OrderBy(b => b).ToListAsync();
        }
    }
}