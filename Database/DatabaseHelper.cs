using System.Collections.ObjectModel;
using CRM.Core;
using CRM.MVVM.M;
using CRM.MVVM.V.AppUser.Controls;

namespace CRM.Database
{
    public class DatabaseHelper
    {
        private readonly DatabaseContext _db;
        private static readonly Lazy<DatabaseHelper> _instance = new(() => new DatabaseHelper());

        public static DatabaseHelper Instance => _instance.Value;

        private DatabaseHelper()
        {
            _db = new DatabaseContext();
            _db.Database.EnsureCreated();
        }
        public void AddUser(User user)
        {
            _db.Users.Add(user);
            Save();
        }
        public void AddNote(Note note)
        {
            _db.Notes.Add(note);
            Save();
        }
        public void AddClient(Client client)
        {
            client.UserId = CurrentUser().UserId;
            client.CreatedAt = DateTime.Now;
            client.UpdatedAt = DateTime.Now;
            _db.Clients.Add(client);
            Save();
        }
        public void AddAppointment(Appointment appointment)
        {
            appointment.UserId = CurrentUser().UserId;
            appointment.CreatedAt = DateTime.Now;
            appointment.AppointmentDate = DateTime.Now;
            _db.Appointments.Add(appointment);
            Save();
        }
        public int GetNextUserId() => _db.Users.Max(p => (int?)p.UserId).GetValueOrDefault(0) + 1;
        public void Save() => _db.SaveChanges();
        public void Discard() => _db.ChangeTracker.Clear();
        public bool HasUnsavedChanges() => _db.ChangeTracker.HasChanges();
        public User CurrentUser() => _db.Users.Where(u => u.UserId == AuthorizationHelper.CurrentUser).First();
        public ObservableCollection<Note> GetNotesForClient(int id) => new ObservableCollection<Note>(_db.Notes.Where(n => n.ClientId == id).ToList());
        public ObservableCollection<Appointment> GetAppointmentsForClient(int clientId) => new ObservableCollection<Appointment>(_db.Appointments.Where(a => a.ClientId == clientId).ToList());
        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
        public int GetUserIdByUsername(string username)
        {
            var user = _db.Users.SingleOrDefault(u => u.Username == username);
            return user.UserId;
        }
        public ObservableCollection<User> GetUsers() => new ObservableCollection<User>(_db.Users.ToList());
        public ObservableCollection<Client> GetClientsById() => new ObservableCollection<Client>(_db.Clients.Where(c => c.UserId == AuthorizationHelper.CurrentUser).ToList());
        public ObservableCollection<Appointment> GetAppointmentsById() => new ObservableCollection<Appointment>(_db.Appointments.Where(a => a.UserId == AuthorizationHelper.CurrentUser).ToList());
        public ObservableCollection<User> SearchUserByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new ObservableCollection<User>();
            }

            var users = _db.Users.AsEnumerable()
                                 .Where(u => u.Username.Contains(name, StringComparison.OrdinalIgnoreCase))
                                 .ToList();
            return new ObservableCollection<User>(users);
        }
        public DatabaseContext GetContext() => _db;
    }
}
