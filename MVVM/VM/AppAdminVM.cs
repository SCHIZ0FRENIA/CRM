using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Core;
using CRM.Database;
using CRM.MVVM.M;
using CRM.MVVM.V.AppUser.Controls.Windows;
using System.Windows;
using CRM.MVVM.V;

namespace CRM.MVVM.VM
{
    class AppAdminVM : NotifyPropertyChanged
    {
        private ObservableCollection<User> _users;
        private bool _isReadOnly = true;
        private DatabaseHelper _db = DatabaseHelper.Instance;
        private string _errorMessage;
        private string _searchString;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set
            {
                _isReadOnly = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }
        public string SearchString
        {
            get { return _searchString; }
            set
            {
                _searchString = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand DiscardCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand LoadCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }

        public AppAdminVM()
        {
            LoadUsers();
            EditCommand = new RelayCommand(EditUsers, CanEditUsers);
            SaveCommand = new RelayCommand(SaveUsers, CanSaveUsers);
            AddCommand = new RelayCommand(AddUser, CanAddUser);
            DiscardCommand = new RelayCommand(DiscardChanges, CanDiscardChanges);
            LoadCommand = new RelayCommand(LoadUsers, CanLoad);
            SearchCommand = new RelayCommand(PerformSearch);
        }

        private void PerformSearch()
        {
            Users = _db.SearchUserByName(SearchString);
        }
        private void LoadUsers() => Users = _db.GetUsers();
        private void EditUsers() => IsReadOnly = false;
        private bool CanEditUsers() => IsReadOnly;
        private void SaveUsers()
        {
            if (ValidateUsers())
            {
                IsReadOnly = true;
                _db.Save();
                LoadUsers();
                ErrorMessage = "";
            }
        }
        private bool CanSaveUsers() => !IsReadOnly;
        private void AddUser()
        {
            Window addUser = new AddUser(new User());
            addUser.ShowDialog();
            LoadUsers();
        }
        private bool CanAddUser() => !IsReadOnly;
        private void DiscardChanges()
        {
            IsReadOnly = true;
            _db.Discard();
            LoadUsers();
        }
        private bool CanDiscardChanges() => _db.HasUnsavedChanges();
        private bool ValidateUsers()
        {
            foreach (var user in Users)
            {
                string error = user.Validate();
                if (!string.IsNullOrEmpty(error))
                {
                    ErrorMessage = user.Username + ": " + error;
                    return false;
                } else if (AuthorizationHelper.UsernameExistsAdmin(user.Username))
                {
                    ErrorMessage = user.Username + ": " + " Duplicate names are not allowed.";
                    return false;
                }
            }
            ErrorMessage = string.Empty;
            return true;
        }
        private bool CanLoad() => IsReadOnly;
    }
}
