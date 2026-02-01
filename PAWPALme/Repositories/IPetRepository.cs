using PAWPALme.Models;

namespace PAWPALme.Repositories
{
    // INTERFACE: The "Contract"
    // This defines the standard set of operations for managing Pets.
    // By using an interface, we decouple the UI from the database logic.
    // This makes it easier to test components by mocking the database response.
    public interface IPetRepository
    {
        // READ operations
        Task<IEnumerable<Pet>> GetAllAsync();
        Task<Pet?> GetByIdAsync(int id);

        // TENANT operation
        // Used by ShelterDetails.razor and Dashboard to isolate data per shelter.
        Task<IEnumerable<Pet>> GetPetsByShelterIdAsync(int shelterId);

        // WRITE operations
        Task AddAsync(Pet pet);
        Task UpdateAsync(Pet pet);
        Task DeleteAsync(int id);
    }
}