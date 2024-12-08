using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_Build.MVVM.M;
using Microsoft.EntityFrameworkCore;

namespace CRM_Build.DB.Repos
{
    public class UsersRepo
    {
        private readonly DbContext _context;
        private readonly DbSet<User> _dbSet;

        public UsersRepo(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<User>();
        }

        // Get all users
        public async Task<List<User>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // Get user by ID
        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Add a new user
        public async Task AddAsync(User user)
        {
            await _dbSet.AddAsync(user);
        }

        // Update an existing user
        public void Update(User user)
        {
            _dbSet.Update(user);
        }

        // Delete a user
        public async Task DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                _dbSet.Remove(user);
            }
        }

        // Find users by criteria
        public async Task<List<User>> FindAsync(System.Linq.Expressions.Expression<System.Func<User, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
    }
}
