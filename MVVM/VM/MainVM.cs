using System.Windows;
using System.Windows.Controls;
using CRM.Core;
using CRM.Database;
using CRM.MVVM.V;
using CRM.MVVM.V.AppUser;
using CRM.MVVM.V.Authorization;

namespace CRM.MVVM.VM
{
    public class MainVM : NotifyPropertyChanged
    {
        private UserControl _currentView;
        private UserControl _mainAuthorization;
        private UserControl _mainAppUser;
        private UserControl _mainAppAdmin;

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
        public MainVM()
        {
            _mainAuthorization = new MainAuthorizationV(StartAppUser, StartAppAdmin);
            CurrentView = _mainAuthorization;
        }

        private void StartAppUser()
        {
            _mainAppUser = new MainAppUser();
            CurrentView = _mainAppUser;
        }
        private void StartAppAdmin()
        {
            _mainAppAdmin = new AppAdmin();
            CurrentView = _mainAppAdmin;
        }
    }
}
