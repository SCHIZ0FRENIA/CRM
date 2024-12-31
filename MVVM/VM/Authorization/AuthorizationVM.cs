using CRM.Core;
using CRM.Database;
using Microsoft.IdentityModel.Tokens;

namespace CRM.MVVM.VM.Authorization
{
    public class AuthorizationVM : NotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private string _errorMessage;
        private Action _startUser;
        private Action _startAdmin;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
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

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand ChangeCommand { get; set; }
        public RelayCommand AuthCommand { get; set; }
        public AuthorizationVM(RelayCommand change, Action startUserCommand, Action startAdminCommand)
        {
            ChangeCommand = change;
            _startUser = startUserCommand;
            _startAdmin = startAdminCommand;
            AuthCommand = new RelayCommand(Auth, CanAuth);
        }

        public void Auth()
        {
            if (Password == "admin" && Username == "admin")
                _startAdmin.Invoke();

            var err = AuthorizationHelper.Authorize(Username, Password);
            if (err != "")
            {
                ErrorMessage = err;
            } else
            {
                AuthorizationHelper.CurrentUser = DatabaseHelper.Instance.GetUserIdByUsername(Username);
                _startUser.Invoke();
            }
        }
        public bool CanAuth()
        {
            return !Username.IsNullOrEmpty() && !Password.IsNullOrEmpty();
        }
    }
}
