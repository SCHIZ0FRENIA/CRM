using CRM.Core;
using CRM.Database;
using CRM.MVVM.M;
using Microsoft.IdentityModel.Tokens;

namespace CRM.MVVM.VM.Authorization
{
    public class RegistrationVM : NotifyPropertyChanged
    {
        private User _user;
        private string _errorMessage;
        private string _passConf;
        private string _password;
        private string _confcode;
        private string _sendedcode;
        private bool _state;
        private bool _notstate;
        private Action _startUser;

        public string ConfCode
        {
            get { return _confcode; }
            set
            {
                _confcode = value;
                OnPropertyChanged();
            }
        }
        public bool State
        {
            get { return _state; }
            set
            {
                _state = value;
                OnPropertyChanged();
            }
        }
        public bool NotState
        {
            get { return _notstate; }
            set
            {
                _notstate = value;
                OnPropertyChanged();
            }
        }
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
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        public string PassConf
        {
            get => _passConf;
            set
            {
                _passConf = value;
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
        public RelayCommand ConfirmEmailCommand { get; set; }
        public RelayCommand RegisterCommand { get; set; }

        public RegistrationVM(RelayCommand change, Action startUserCommand)
        {
            ChangeCommand = change;
            State = false;
            NotState = true;
            _user = new User();
            ConfirmEmailCommand = new RelayCommand(ConfirmEmail, CanSend);
            RegisterCommand = new RelayCommand(Register, CanRegister);
            _startUser = startUserCommand;
        }

        private void ConfirmEmail()
        {
            State = true;
            NotState = false;
            _sendedcode = Mailer.SendConfirmationCode(Email,
                "Confirmation code for CRM",
                $"Hello {Username}, this is your confirmation code:", ".");
        }
        private bool CanSend()
        {
            ErrorMessage = _user.Validate();

            if (AuthorizationHelper.UsernameExists(Username))
            {
                ErrorMessage = "Username already in use.";
                return false;
            }

            if (_passConf != _password || !AuthorizationHelper.IsValidPassword(_password))
            {
                ErrorMessage = "Check your password, it must contain 8 symbols.";
                return false;
            }

            return string.IsNullOrEmpty(ErrorMessage);
        }
        private void Register() 
        {            
            _user.Salt = AuthorizationHelper.GenerateSalt();
            _user.PasswordHash = AuthorizationHelper.HashPassword(Password, _user.Salt);
            DatabaseHelper.Instance.AddUser
            (
                new User
                (
                    Username, 
                    _user.PasswordHash, 
                    _user.Salt, 
                    Email,
                    DateTime.Now
                )
            );
            AuthorizationHelper.CurrentUser = DatabaseHelper.Instance.GetUserIdByUsername(Username);
            _startUser.Invoke();
        }
        private bool CanRegister()
        {
            if (NotState)
                return false;
            else if (_confcode != _sendedcode)
            {
                ErrorMessage = "Check your confirmation code.";
                return false;
            }

            return string.IsNullOrEmpty(ErrorMessage);
        }
    }
}
