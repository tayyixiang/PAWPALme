using PAWPALme.Models;

namespace PAWPALme.Repositories
{
    // INTERFACE: The "Contract"
    // This defines exactly what appointment operations our app is capable of performing.
    // By using an interface, we can easily swap out the database later (e.g., for unit testing)
    // without breaking the rest of the application.
    public interface IAppointmentRepository
    {
        // COMMANDS (Write Data)
        Task AddAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(int id); // Added per your request for cancellation features

        // QUERIES (Read Data)
        Task<Appointment?> GetByIdAsync(int id);

        // Tenant Query: Used by Shelters to see their schedule
        Task<IEnumerable<Appointment>> GetByShelterIdAsync(int shelterId);

        // User Query: Used by Adopters to see "My Appointments"
        Task<IEnumerable<Appointment>> GetByUserIdAsync(string userId);
    }
}