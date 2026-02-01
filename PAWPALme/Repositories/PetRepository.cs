using Microsoft.EntityFrameworkCore;
using PAWPALme.Data;
using PAWPALme.Models;

namespace PAWPALme.Repositories
{
    public class PetRepository : IPetRepository
    {
        // ARCHITECTURE: Injects the Database Context to access the SQL Server
        private readonly ApplicationDbContext _context;

        public PetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pet>> GetAllAsync()
        {
            // PERFORMANCE: Eager Loading (.Include)
            // Fetches the associated Shelter data in the same SQL query to avoid "N+1 Select" issues
            return await _context.Pets
                .Include(p => p.Shelter)
                .ToListAsync();
        }

        public async Task<Pet?> GetByIdAsync(int id)
        {
            return await _context.Pets
                .Include(p => p.Shelter)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        // LOGIC: Filters pets by a specific Shelter ID
        // Used by the Shelter Dashboard to ensure tenants only manage their own inventory.
        public async Task<IEnumerable<Pet>> GetPetsByShelterIdAsync(int shelterId)
        {
            return await _context.Pets
                .Where(p => p.ShelterId == shelterId)
                .OrderByDescending(p => p.Id) // Show newest pets first
                .ToListAsync();
        }

        public async Task AddAsync(Pet pet)
        {
            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Pet pet)
        {
            // STATE MANAGEMENT: Clears the ChangeTracker to prevent conflicts 
            // if the entity was previously loaded in the same context scope.
            _context.ChangeTracker.Clear();
            _context.Pets.Update(pet);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet != null)
            {
                _context.Pets.Remove(pet);
                await _context.SaveChangesAsync();
            }
        }
    }
}