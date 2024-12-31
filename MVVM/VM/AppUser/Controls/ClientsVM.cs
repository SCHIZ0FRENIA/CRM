using CRM.Core;
using CRM.Database;
using CRM.MVVM.M;
using CRM.MVVM.V.AppUser.Controls.Windows;
using System.Collections.ObjectModel;
using System.Windows;

namespace CRM.MVVM.VM.AppUser.Controls
{
    public class ClientsVM : NotifyPropertyChanged
    {
        private ObservableCollection<Client> _clients;
        private bool _isReadOnly = true;
        private DatabaseHelper _db;
        private string _errorMessage;

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
        public ObservableCollection<Client> Clients
        {
            get => _clients;
            set
            {
                _clients = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand DiscardCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand LoadCommand { get; set; }

        public ClientsVM()
        {
            _db = DatabaseHelper.Instance;
            LoadClients();
            EditCommand = new RelayCommand(EditClients, CanEditClients);
            SaveCommand = new RelayCommand(SaveClients, CanSaveClients);
            AddCommand = new RelayCommand(AddClient, CanAddClient);
            DiscardCommand = new RelayCommand(DiscardChanges, CanDiscardChanges);
            LoadCommand = new RelayCommand(LoadClients, CanLoad);
        }

        private void LoadClients() => Clients = DatabaseHelper.Instance.GetClientsById();
        private void EditClients() => IsReadOnly = false;
        private bool CanEditClients() => IsReadOnly;
        private void SaveClients()
        {
            if (ValidateClients())
            {
                IsReadOnly = true;
                _db.Save();
                LoadClients();
            }
        }
        private bool CanSaveClients() => !IsReadOnly;
        private void AddClient()
        {
            Window addClient = new AddClient(new Client());
            addClient.ShowDialog();
            LoadClients();
        }
        private bool CanAddClient() => !IsReadOnly;
        private void DiscardChanges()
        {
            IsReadOnly = true;
            _db.Discard();
            LoadClients();
        }
        private bool CanDiscardChanges() => _db.HasUnsavedChanges();
        private bool ValidateClients()
        {
            foreach (var client in Clients)
            {
                string error = client.Validate();
                if (!string.IsNullOrEmpty(error))
                {
                    ErrorMessage = client.FirstName + ' ' + client.LastName + ": " + error;
                    return false;
                }
            }
            ErrorMessage = string.Empty;
            return true;
        }
        private bool CanLoad() => IsReadOnly;
    }
}
