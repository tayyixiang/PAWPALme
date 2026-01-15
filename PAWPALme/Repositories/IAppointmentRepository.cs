using PAWPALme.Models;

namespace PAWPALme.Repositories
{
    public interface IAppointmentRepository
    {
        Task AddAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
        Task<Appointment?> GetByIdAsync(int id);
        Task<IEnumerable<Appointment>> GetByShelterIdAsync(int shelterId); // For Shelter
        Task<IEnumerable<Appointment>> GetByUserIdAsync(string userId);    // For Adopter
    }
}