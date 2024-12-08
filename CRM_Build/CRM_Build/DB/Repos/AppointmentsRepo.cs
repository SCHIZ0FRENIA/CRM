using CRM_Build.MVVM.M;
using Microsoft.EntityFrameworkCore;

namespace CRM_Build.DB.Repos
{
    public class AppointmentsRepo
    {
        private readonly DbContext _context;
        private readonly DbSet<Appointment> _dbSet;

        public AppointmentsRepo(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Appointment>();
        }

        // Get all appointments
        public async Task<List<Appointment>> GetAllAsync()
        {
            return await _dbSet.Include(a => a.User).ToListAsync();
        }

        // Get appointment by ID
        public async Task<Appointment> GetByIdAsync(int id)
        {
            return await _dbSet.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == id);
        }

        // Add new appointment
        public async Task AddAsync(Appointment appointment)
        {
            await _dbSet.AddAsync(appointment);
        }

        // Update appointment
        public void Update(Appointment appointment)
        {
            _dbSet.Update(appointment);
        }

        // Delete appointment by ID
        public async Task DeleteAsync(int id)
        {
            var appointment = await GetByIdAsync(id);
            if (appointment != null)
            {
                _dbSet.Remove(appointment);
            }
        }

        // Find appointments by criteria
        public async Task<List<Appointment>> FindAsync(
            System.Linq.Expressions.Expression<Func<Appointment, bool>> predicate)
        {
            return await _dbSet.Include(a => a.User).Where(predicate).ToListAsync();
        }
    }
}
