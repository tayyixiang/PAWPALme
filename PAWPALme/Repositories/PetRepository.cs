using Microsoft.EntityFrameworkCore;
using PAWPALme.Data;
using PAWPALme.Models;

namespace PAWPALme.Repositories
{
    // IMPLEMENTATION: The "Worker"
    // This class executes the actual SQL commands using Entity Framework Core.
    public class PetRepository : IPetRepository
    {
        // DEPENDENCY INJECTION:
        // We inject the database context so this repository can talk to SQL Server.
        private readonly ApplicationDbContext _context;

        public PetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- PUBLIC QUERY: BROWSE ALL PETS ---
        public async Task<IEnumerable<Pet>> GetAllAsync()
        {
            // PERFORMANCE: Eager Loading (.Include)
            // We fetch the associated Shelter data in the same SQL query.
            // This prevents the "N+1 Select Problem" where the app would otherwise 
            // query the database 50 times to get 50 shelter names for the list.
            return await _context.Pets
                .Include(p => p.Shelter)
                .ToListAsync();
        }

        // --- SINGLE ITEM QUERY: DETAILS PAGE ---
        public async Task<Pet?> GetByIdAsync(int id)
        {
            // FirstOrDefaultAsync returns null if the ID is invalid (e.g., pet deleted),
            // allowing the UI to handle the 404 error gracefully.
            return await _context.Pets
                .Include(p => p.Shelter)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // --- TENANT QUERY: SHELTER DASHBOARD ---
        // Filters pets by a specific Shelter ID.
        // Used by the Dashboard to ensure Tenant A cannot see Tenant B's inventory.
        public async Task<IEnumerable<Pet>> GetPetsByShelterIdAsync(int shelterId)
        {
            return await _context.Pets
                .Where(p => p.ShelterId == shelterId) // The Filter
                .OrderByDescending(p => p.Id)         // The Sort (Newest First)
                .ToListAsync();
        }

        // --- WRITE: CREATE ---
        public async Task AddAsync(Pet pet)
        {
            _context.Pets.Add(pet);
            await _context.SaveChangesAsync(); // Commits the transaction
        }

        // --- WRITE: UPDATE ---
        public async Task UpdateAsync(Pet pet)
        {
            // STATE MANAGEMENT FIX: 
            // Sometimes Blazor holds onto old versions of an object in memory.
            // ChangeTracker.Clear() wipes the memory slate clean to prevent 
            // "The instance of entity type cannot be tracked" errors during updates.
            _context.ChangeTracker.Clear();

            _context.Pets.Update(pet);
            await _context.SaveChangesAsync();
        }

        // --- WRITE: DELETE ---
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