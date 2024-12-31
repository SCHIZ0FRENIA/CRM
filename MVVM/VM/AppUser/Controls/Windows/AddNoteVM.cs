using System.Windows;
using CRM.Core;
using CRM.Database;
using CRM.MVVM.M;
using Microsoft.IdentityModel.Tokens;

namespace CRM.MVVM.VM.AppUser.Controls.Windows
{
    public class AddNoteVM : NotifyPropertyChanged
    {
        private Note _note;
        private Window _window;
        private string _errorMessage;
        private int _clientId;
        DatabaseHelper _db = DatabaseHelper.Instance;

        public string NoteName
        {
            get => _note.NoteName;
            set
            {
                _note.NoteName = value;
                OnPropertyChanged();
            }
        }
        public string NoteText
        {
            get => _note.NoteText;
            set
            {
                _note.NoteText = value;
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

        public AddNoteVM(Note note, Window window, int id)
        {
            _note = note;
            _window = window;
            _clientId = id;
            SaveCommand = new RelayCommand(SaveNote, CanSaveNote);
            CancelCommand = new RelayCommand(CancelNote);
        }

        private void SaveNote()
        {
            _note.ClientId = _clientId;
            _db.AddNote(_note);
            _window.Close();
        }
        private bool CanSaveNote()
        {
            ErrorMessage = _note.Validate();
            return ErrorMessage.IsNullOrEmpty();
        }
        private void CancelNote()
        {
            _window.Close();
        }
    }
}
