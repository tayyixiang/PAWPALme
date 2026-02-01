using Microsoft.EntityFrameworkCore;
using PAWPALme.Data;
using PAWPALme.Models;

namespace PAWPALme.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Used when selecting a specific appointment to Edit or View Details
        public async Task<Appointment?> GetByIdAsync(int id)
        {
            // JOIN LOGIC: Pulls in both the Pet details and the Adopter (User) details
            return await _context.Appointments
                .Include(a => a.Pet)
                .Include(a => a.AdopterUser)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        // QUERY: Fetch by Tenant (Shelter)
        // Used by the Shelter Dashboard to see incoming requests
        public async Task<IEnumerable<Appointment>> GetByShelterIdAsync(int shelterId)
        {
            return await _context.Appointments
                .Include(a => a.Pet)
                .Include(a => a.AdopterUser)
                .Where(a => a.ShelterId == shelterId) // WHERE Clause filtering
                .OrderByDescending(a => a.AppointmentDate) // Sort by Date
                .ToListAsync();
        }

        // QUERY: Fetch by User
        // Used by "My Appointments" page so adopters can track their own history
        public async Task<IEnumerable<Appointment>> GetByUserIdAsync(string userId)
        {
            return await _context.Appointments
                .Include(a => a.Pet)
                .Include(a => a.Shelter)
                .Where(a => a.AdopterUserId == userId)
                .OrderByDescending(a => a.AppointmentDate)
                .ToListAsync();
        }

        public async Task AddAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var appt = await _context.Appointments.FindAsync(id);
            if (appt != null)
            {
                _context.Appointments.Remove(appt);
                await _context.SaveChangesAsync();
            }
        }
    }
}