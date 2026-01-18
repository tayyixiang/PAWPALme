using Microsoft.EntityFrameworkCore;
using PAWPALme.Data;
using PAWPALme.Models;

namespace PAWPALme.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public AppointmentRepository(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public async Task AddAsync(Appointment appointment)
        {
            using var context = _factory.CreateDbContext();
            context.Appointment.Add(appointment);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            using var context = _factory.CreateDbContext();
            context.Appointment.Update(appointment);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _factory.CreateDbContext();
            var appt = await context.Appointment.FindAsync(id);
            if (appt != null)
            {
                context.Appointment.Remove(appt);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Appointment?> GetByIdAsync(int id)
        {
            using var context = _factory.CreateDbContext();
            return await context.Appointment
                .Include(a => a.Pet)
                .Include(a => a.Shelter)
                .Include(a => a.AdoptionApplication)
                .Include(a => a.AdopterUser) // Ensure User is loaded
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Appointment>> GetByShelterIdAsync(int shelterId)
        {
            using var context = _factory.CreateDbContext();
            return await context.Appointment
                .Include(a => a.Pet)
                .Include(a => a.AdoptionApplication)
                .Include(a => a.AdopterUser) // <--- CRITICAL FIX: Loads Adopter Name
                .Where(a => a.ShelterId == shelterId)
                .OrderByDescending(a => a.AppointmentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByUserIdAsync(string userId)
        {
            using var context = _factory.CreateDbContext();
            return await context.Appointment
                .Include(a => a.Pet)
                .Include(a => a.Shelter)
                .Where(a => a.AdopterUserId == userId)
                .OrderByDescending(a => a.AppointmentDate)
                .ToListAsync();
        }
    }
}