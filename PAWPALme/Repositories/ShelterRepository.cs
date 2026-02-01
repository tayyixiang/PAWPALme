using Microsoft.EntityFrameworkCore;
using PAWPALme.Data;
using PAWPALme.Models;

namespace PAWPALme.Repositories
{
    public class ShelterRepository : IShelterRepository
    {
        private readonly ApplicationDbContext _context;

        public ShelterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Shelter>> GetAllAsync()
        {
            // Asynchronously fetches all shelters from the database
            return await _context.Shelters.ToListAsync();
        }

        public async Task<Shelter?> GetByIdAsync(int id)
        {
            // Eager Loading: Fetches the Shelter AND its associated Pets in a single query
            // This prevents "NullReferenceException" when trying to access shelter.Pets later
            return await _context.Shelters
                .Include(s => s.Pets)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Shelter?> GetByUserIdAsync(string userId)
        {
            // Finds the specific shelter profile linked to the logged-in user's ID
            return await _context.Shelters
                .FirstOrDefaultAsync(s => s.OwnerUserId == userId);
        }

        public async Task<bool> UserHasShelterAsync(string userId)
        {
            // Efficient check (EXISTS query) to see if a user has already registered a shelter
            return await _context.Shelters.AnyAsync(s => s.OwnerUserId == userId);
        }

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