using PAWPALme.Models;

namespace PAWPALme.Repositories
{
    public interface IPetRepository
    {
        Task<IEnumerable<Pet>> GetAllAsync();
        Task<Pet?> GetByIdAsync(int id);

        // This is the specific method required by ShelterDetails
        Task<IEnumerable<Pet>> GetPetsByShelterIdAsync(int shelterId);

        Task AddAsync(Pet pet);
        Task UpdateAsync(Pet pet);
        Task DeleteAsync(int id);
    }
}