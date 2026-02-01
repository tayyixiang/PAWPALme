using PAWPALme.Models;

namespace PAWPALme.Repositories
{
    // INTERFACE: The "Blueprint"
    // This defines the specific capabilities required to manage Shelters.
    // It's separated from the implementation so we can modify the database logic 
    // without breaking the rest of the application.
    public interface IShelterRepository
    {
        // QUERIES
        Task<IEnumerable<Shelter>> GetAllAsync(); // Used for the "Our Partners" public page
        Task<Shelter?> GetByIdAsync(int id);      // Used for the specific Shelter Profile page

        // SECURITY QUERY
        // Used to find which shelter belongs to the currently logged-in user.
        Task<Shelter?> GetByUserIdAsync(string userId);

        // VALIDATION QUERY
        // Used during registration to prevent one user from owning multiple shelters.
        Task<bool> UserHasShelterAsync(string userId);

        // COMMANDS
        Task AddAsync(Shelter shelter);
        Task UpdateAsync(Shelter shelter);
        // Note: Delete is usually restricted for Shelters to prevent data loss (orphaned pets).
    }
}