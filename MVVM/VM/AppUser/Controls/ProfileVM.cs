using CRM.Core;
using CRM.Database;
using CRM.MVVM.M;

namespace CRM.MVVM.VM.AppUser.Controls
{
    public class ProfileVM : NotifyPropertyChanged
    {
        private User _user;
        private DatabaseHelper _db;

        public string Username
        {
            get => _user.Username;
            set
            {
                _user.Username = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get => _user.Email;
            set
            {
                _user.Email = value;
                OnPropertyChanged();
            }
        }

        public ProfileVM()
        {
            _db = DatabaseHelper.Instance;
            _user = _db.CurrentUser();
        }
    }
}
