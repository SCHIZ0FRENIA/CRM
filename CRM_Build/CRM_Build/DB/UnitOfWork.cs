using CRM_Build.DB.Repos;
using Microsoft.EntityFrameworkCore;

namespace CRM_Build.DB
{
    public class UnitOfWork : DbContext
    {
        private readonly string _connectionString;
        private bool _disposed;

        public UnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public AppointmentsRepo Appointments => new AppointmentsRepo(this);
        public UsersRepo Users => new UsersRepo(this);

        public async Task CommitAsync()
        {
            await SaveChangesAsync();
        }

        public void Commit()
        {
            SaveChanges();
        }

        public new void Dispose()
        {
            if (!_disposed)
            {
                base.Dispose();
                _disposed = true;
            }
        }
    }
}
