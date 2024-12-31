using System.Windows;
using CRM.Core;
using CRM.Database;
using CRM.MVVM.M;
using Microsoft.IdentityModel.Tokens;

namespace CRM.MVVM.VM
{
    public class AddUserVM : NotifyPropertyChanged
    {
        private User _user;
        private Window _window;
        private string _errorMessage;
        private DatabaseHelper _db = DatabaseHelper.Instance;
        private string _password;

        public string Username
        {
            get => _user.Username;
            set
            {
                _user.Username = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
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
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public AddUserVM(User user, Window window)
        {
            _user = user;
            _window = window;
            SaveCommand = new RelayCommand(SaveUser, CanSaveUser);
            CancelCommand = new RelayCommand(CancelUser);
        }

        private void SaveUser()
        {
            var salt = AuthorizationHelper.GenerateSalt();
            var passwordHash = AuthorizationHelper.HashPassword(_password, salt);

            _user.Salt = salt;
            _user.PasswordHash = passwordHash;

            _db.AddUser(_user);
            _window.Close();
        }

        private bool CanSaveUser()
        {
            ErrorMessage = _user.Validate();
            return ErrorMessage.IsNullOrEmpty();
        }

        private void CancelUser()
        {
            _window.Close();
        }
    }
}
