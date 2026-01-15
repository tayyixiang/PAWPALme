using PAWPALme.Models;

namespace PAWPALme.Repositories
{
    public interface IPetRepository
    {
        Task<Pet?> GetByIdAsync(int id);
        Task<IEnumerable<Pet>> GetAllAsync();
        Task<IEnumerable<Pet>> GetPetsByShelterIdAsync(int shelterId);
        Task AddAsync(Pet pet);
        Task UpdateAsync(Pet pet);
        Task DeleteAsync(int id);

        // Required for Search/Filter
        Task<IEnumerable<Pet>> SearchAsync(string? searchTerm, string? species, string? breed, int? age);
        Task<IEnumerable<string>> GetBreedsAsync();
    }
}