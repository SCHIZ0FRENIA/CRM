using CRM.Core;
using CRM.MVVM.V.Authorization;
using System.Windows.Controls;

namespace CRM.MVVM.VM.Authorization
{
    public class MainAuthorizationVM : NotifyPropertyChanged
    {
        private UserControl _currentView;
        private UserControl _authorizationView;
        private UserControl _registrationView;
        private RelayCommand _ChangeAuthViewCommand;
        public UserControl CurrentView
        {
            get => _currentView;
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChanged(nameof(CurrentView));
                }
            }
        }

        public MainAuthorizationVM(Action StartUserCommand, Action StartAdminCommand)
        {
            _ChangeAuthViewCommand = new RelayCommand(ChangeAuthView);
            _authorizationView = new AuthorizationV(_ChangeAuthViewCommand, StartUserCommand, StartAdminCommand);
            _registrationView = new RegistrationV(_ChangeAuthViewCommand, StartUserCommand);
            CurrentView = _authorizationView;
        }

        private void ChangeAuthView()
        {
            if (this.CurrentView != _authorizationView)
            {
                CurrentView = _authorizationView;
            }
            else
            {
                CurrentView = _registrationView;
            }
        }
    }
}
