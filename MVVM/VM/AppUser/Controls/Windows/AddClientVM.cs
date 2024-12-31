using CRM.Core;
using CRM.Database;
using System.Windows;
using CRM.MVVM.M;
using Microsoft.IdentityModel.Tokens;

namespace CRM.MVVM.VM.AppUser.Controls.Windows
{
    public class AddClientVM : NotifyPropertyChanged
    {
        private Client _client;
        private Window _window;
        private string _errorMessage;
        private DatabaseHelper _db = DatabaseHelper.Instance;

        public string FirstName
        {
            get => _client.FirstName;
            set
            {
                _client.FirstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => _client.LastName;
            set
            {
                _client.LastName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _client.Email;
            set
            {
                _client.Email = value;
                OnPropertyChanged();
            }
        }

        public string PhoneNumber
        {
            get => _client.PhoneNumber;
            set
            {
                _client.PhoneNumber = value;
                OnPropertyChanged();
            }
        }

        public string CompanyName
        {
            get => _client.CompanyName;
            set
            {
                _client.CompanyName = value;
                OnPropertyChanged();
            }
        }

        public string JobTitle
        {
            get => _client.JobTitle;
            set
            {
                _client.JobTitle = value;
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

        public AddClientVM(Client client, Window window)
        {
            _client = client;
            _window = window;
            SaveCommand = new RelayCommand(SaveClient, CanSaveClient);
            CancelCommand = new RelayCommand(CancelClient);
        }

        private void SaveClient()
        {
            _db.AddClient(_client);
            _window.Close();
        }

        private bool CanSaveClient()
        {
            ErrorMessage = _client.Validate();
            return ErrorMessage.IsNullOrEmpty();
        }

        private void CancelClient()
        {
            _window.Close();
        }
    }
}
