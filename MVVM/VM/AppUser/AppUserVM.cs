using System.Windows.Controls;
using CRM.Core;
using CRM.MVVM.V.AppUser.Controls;

namespace CRM.MVVM.VM.AppUser
{
    public class AppUserVM : NotifyPropertyChanged
    {
        private ClientsV _clientsControl;
        private CalendarV _calendarControl;
        private ProfileV _profileControl;
        private NotesV _notesControl;
        private UserControl _currentControl;

        public UserControl CurrentControl
        {
            get { return _currentControl; }
            set 
            { 
                _currentControl = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand ChangeToClientsCommand { get; set; }
        public RelayCommand ChangeToCalendarCommand { get; set; }
        public RelayCommand ChangeToProfileCommand { get; set; }
        public RelayCommand ChangeToNotesCommand { get; set; }

        public AppUserVM()
        {
            ChangeToClientsCommand = new RelayCommand(ChangeToClients);
            ChangeToCalendarCommand = new RelayCommand(ChangeToCalendar);
            ChangeToProfileCommand = new RelayCommand(ChangeToProfile);
            ChangeToNotesCommand = new RelayCommand(ChangeToNotes);
            _clientsControl = new ClientsV();
            _calendarControl = new CalendarV();
            _profileControl = new ProfileV();
            _notesControl = new NotesV();
            CurrentControl = _clientsControl;
        }

        private void ChangeToClients()
        {
            if (CurrentControl != _clientsControl)
            {
                CurrentControl = _clientsControl;
            }
        }
        private void ChangeToCalendar()
        {
            if (CurrentControl != _calendarControl)
            {
                CurrentControl = _calendarControl;
            }
        }
        private void ChangeToProfile()
        {
            if (CurrentControl != _profileControl)
            {
                CurrentControl = _profileControl;
            }
        }
        private void ChangeToNotes()
        {
            if (CurrentControl != _notesControl)
            {
                CurrentControl = _notesControl;
            }
        }
    }
}
