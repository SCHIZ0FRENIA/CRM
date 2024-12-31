using CRM.Core;
using CRM.Database;
using System.Windows;
using CRM.MVVM.M;
using Microsoft.IdentityModel.Tokens;

namespace CRM.MVVM.VM.AppUser.Controls.Windows
{
    public class AddAppointmentVM : NotifyPropertyChanged
    {
        private Appointment _appointment;
        private Window _window;
        private string _errorMessage;
        private int _clientId;
        private DatabaseHelper _db = DatabaseHelper.Instance;

        public DateTime AppointmentDate
        {
            get => _appointment.AppointmentDate;
            set
            {
                _appointment.AppointmentDate = value;
                OnPropertyChanged();
            }
        }

        public string Subject
        {
            get => _appointment.Subject;
            set
            {
                _appointment.Subject = value;
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

        public AddAppointmentVM(Appointment appointment, Window window, int id)
        {
            _appointment = appointment;
            _window = window;
            _clientId = id;
            SaveCommand = new RelayCommand(SaveAppointment, CanSaveAppointment);
            CancelCommand = new RelayCommand(CancelAppointment);
        }

        private void SaveAppointment()
        {
            _appointment.ClientId = _clientId;
            _db.AddAppointment(_appointment);
            _window.Close();
        }

        private bool CanSaveAppointment()
        {
            ErrorMessage = _appointment.Validate();
            return ErrorMessage.IsNullOrEmpty();
        }

        private void CancelAppointment()
        {
            _window.Close();
        }
    }
}
