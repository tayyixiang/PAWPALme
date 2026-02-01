using Microsoft.EntityFrameworkCore;
using PAWPALme.Data;
using PAWPALme.Models;

namespace PAWPALme.Repositories
{
    // IMPLEMENTATION: The "Worker"
    // This class performs the actual SQL database work defined in the Interface.
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        // DEPENDENCY INJECTION:
        // We ask the system for the database connection (_context) automatically.
        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- SINGLE ITEM FETCH ---
        // Used when clicking "View Details" on a specific booking.
        public async Task<Appointment?> GetByIdAsync(int id)
        {
            // EAGER LOADING (.Include):
            // When we load the appointment, we tell SQL to also grab the related data 
            // in the same query. This prevents "NullReferenceException" when we try to 
            // display the Pet Name or User Name on the details page.
            return await _context.Appointments
                .Include(a => a.Pet)
                .Include(a => a.AdopterUser)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        // --- SHELTER DASHBOARD QUERY ---
        // Used by the Shelter Dashboard to show a list of incoming requests.
        public async Task<IEnumerable<Appointment>> GetByShelterIdAsync(int shelterId)
        {
            return await _context.Appointments
                .Include(a => a.Pet)         // Need pet details to show which animal is being visited
                .Include(a => a.AdopterUser) // Need user details to show WHO is coming
                .Where(a => a.ShelterId == shelterId) // FILTER: Security check (Shelter A can't see Shelter B's bookings)
                .OrderByDescending(a => a.AppointmentDate) // SORT: Show newest/upcoming first
                .ToListAsync();
        }

        // --- USER HISTORY QUERY ---
        // Used by the "My Appointments" page for regular users.
        public async Task<IEnumerable<Appointment>> GetByUserIdAsync(string userId)
        {
            return await _context.Appointments
                .Include(a => a.Pet)
                .Include(a => a.Shelter) // Include Shelter info so the user knows where to go (Address)
                .Where(a => a.AdopterUserId == userId) // FILTER: User can only see their own bookings
                .OrderByDescending(a => a.AppointmentDate)
                .ToListAsync();
        }

        // --- WRITE OPERATIONS ---

        public async Task AddAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync(); // Commits the INSERT statement to SQL
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync(); // Commits the UPDATE statement to SQL
        }

        public async Task DeleteAsync(int id)
        {
            // 1. Find the item
            var appt = await _context.Appointments.FindAsync(id);
            if (appt != null)
            {
                // 2. Remove it locally
                _context.Appointments.Remove(appt);
                // 3. Commit to Database
                await _context.SaveChangesAsync();
            }
        }
    }
}