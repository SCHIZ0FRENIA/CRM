using CRM.Core;
using CRM.Database;
using CRM.MVVM.M;
using CRM.MVVM.V.AppUser.Controls.Windows;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace CRM.MVVM.VM.AppUser.Controls
{
    public class CalendarVM : NotifyPropertyChanged
    {
        private ObservableCollection<Client> _clients;
        private ObservableCollection<Appointment> _appointments;
        private Client _selectedClient;
        private Appointment _selectedAppointment;
        private bool _isEditing;
        private DatabaseHelper _db = DatabaseHelper.Instance;

        public bool IsEditing
        {
            get { return _isEditing; }
            set
            {
                _isEditing = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Client> Clients
        {
            get => _clients;
            set
            {
                if (_clients != value)
                {
                    _clients = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<Appointment> Appointments
        {
            get => _appointments;
            set
            {
                if (_appointments != value)
                {
                    _appointments = value;
                    OnPropertyChanged();
                }
            }
        }
        public Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                if (_selectedClient != value)
                {
                    _selectedClient = value;
                    OnPropertyChanged();
                    LoadAppointmentsForClient(value);
                }
            }
        }
        public Appointment SelectedAppointment
        {
            get => _selectedAppointment;
            set
            {
                if (_selectedAppointment != value)
                {
                    _selectedAppointment = value;
                    OnPropertyChanged();
                    Debug.WriteLine($"SelectedAppointment changed to: {_selectedAppointment?.Subject}");
                }
            }
        }

        public RelayCommandWithParam SelectClientCommand { get; set; }
        public RelayCommandWithParam SelectAppointmentCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand DiscardCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand EditCommand { get; set; }

        public CalendarVM()
        {
            IsEditing = false;
            SelectClientCommand = new RelayCommandWithParam(SelectClient);
            SelectAppointmentCommand = new RelayCommandWithParam(SelectAppointment);
            SaveCommand = new RelayCommand(SaveChanges, CanSaveChanges);
            DiscardCommand = new RelayCommand(DiscardChanges, CanDiscardChanges);
            AddCommand = new RelayCommand(AddAppointment, CanAddAppointment);
            EditCommand = new RelayCommand(EditAppointment, CanEditAppointment);
            LoadClients();
        }

        private void EditAppointment()
        {
            IsEditing = true;
        }
        private bool CanEditAppointment()
        {
            return !IsEditing && SelectedAppointment != null;
        }
        private void AddAppointment()
        {
            Window addAppointment = new AddAppointment(new Appointment(), SelectedClient.ClientId);
            addAppointment.ShowDialog();
            LoadAppointmentsForClient(SelectedClient);
        }
        private bool CanAddAppointment()
        {
            return !IsEditing && SelectedClient != null;
        }
        private void DiscardChanges()
        {
            _db.Discard();
            LoadAppointmentsForClient(SelectedClient);
            IsEditing = false;
        }
        private bool CanDiscardChanges()
        {
            return IsEditing;
        }
        private void SaveChanges()
        {
            if (!SelectedAppointment.Subject.IsNullOrEmpty())
            {
                _db.Save();
                IsEditing = false;
            }
        }
        private bool CanSaveChanges()
        {
            return IsEditing;
        }
        private void LoadClients()
        {
            Clients = _db.GetClientsById();
        }
        private void LoadAppointmentsForClient(Client client)
        {
            Appointments = _db.GetAppointmentsForClient(client.ClientId);
        }
        private void SelectClient(object parameter)
        {
            if (parameter is Client client)
            {
                SelectedClient = client;
            }
        }
        private void SelectAppointment(object parameter)
        {
            if (parameter is Appointment appointment)
            {
                SelectedAppointment = appointment;
            }
        }
    }
}
