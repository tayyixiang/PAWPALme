using Microsoft.EntityFrameworkCore;
using PAWPALme.Data;
using PAWPALme.Models;

namespace PAWPALme.Repositories
{
    // IMPLEMENTATION: The "Database Manager"
    public class ShelterRepository : IShelterRepository
    {
        private readonly ApplicationDbContext _context;

        public ShelterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- PUBLIC LISTING ---
        // Fetches the list for the "Our Partners" page.
        public async Task<IEnumerable<Shelter>> GetAllAsync()
        {
            return await _context.Shelters.ToListAsync();
        }

        // --- PROFILE DETAILS ---
        public async Task<Shelter?> GetByIdAsync(int id)
        {
            // EAGER LOADING (.Include):
            // When we load a Shelter profile, we almost always want to see their pets too.
            // This command executes a SQL JOIN to fetch the Shelter AND their Pets in one go.
            return await _context.Shelters
                .Include(s => s.Pets)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        // --- OWNER LOOKUP ---
        // This is the link between the Login System (UserId) and the Business Data (Shelter).
        // It allows the Dashboard to say "Welcome back, [Shelter Name]" instead of just "Welcome User".
        public async Task<Shelter?> GetByUserIdAsync(string userId)
        {
            return await _context.Shelters
                .FirstOrDefaultAsync(s => s.OwnerUserId == userId);
        }

        // --- REGISTRATION CHECK ---
        // An efficient SQL "EXISTS" query.
        // It returns True/False immediately without loading the heavy shelter object into memory.
        public async Task<bool> UserHasShelterAsync(string userId)
        {
            return await _context.Shelters.AnyAsync(s => s.OwnerUserId == userId);
        }

        // --- WRITE OPERATIONS ---

        public async Task AddAsync(Shelter shelter)
        {
            _context.Shelters.Add(shelter);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Shelter shelter)
        {
            _context.Shelters.Update(shelter);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var shelter = await _context.Shelters.FindAsync(id);
            if (shelter != null)
            {
                _context.Shelters.Remove(shelter);
                await _context.SaveChangesAsync();
            }
        }
    }
}