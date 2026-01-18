using PAWPALme.Models;

namespace PAWPALme.Repositories
{
    public interface IAppointmentRepository
    {
        Task AddAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(int id); // <--- FIXED: Added DeleteAsync
        Task<Appointment?> GetByIdAsync(int id);
        Task<IEnumerable<Appointment>> GetByShelterIdAsync(int shelterId);
        Task<IEnumerable<Appointment>> GetByUserIdAsync(string userId);
    }
}