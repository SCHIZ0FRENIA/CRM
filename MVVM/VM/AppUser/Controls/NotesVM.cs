using CRM.Core;
using CRM.Database;
using CRM.MVVM.VM.AppUser.Controls.Windows;
using CRM.MVVM.M;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using CRM.MVVM.V.AppUser.Controls.Windows;

namespace CRM.MVVM.VM.AppUser.Controls
{
    public class NotesVM : NotifyPropertyChanged
    {
        private ObservableCollection<Client> _clients;
        private ObservableCollection<Note> _notes;
        private Client _selectedClient;
        private Note _selectedNote;
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
        public ObservableCollection<Note> Notes
        {
            get => _notes;
            set
            {
                if (_notes != value)
                {
                    _notes = value;
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
                    LoadNotesForClient(value);
                }
            }
        }
        public Note SelectedNote
        {
            get => _selectedNote;
            set
            {
                if (_selectedNote != value)
                {
                    _selectedNote = value;
                    OnPropertyChanged(); Debug.WriteLine($"SelectedNote changed to: {_selectedNote?.NoteName}");
                }
            }
        }

        public RelayCommandWithParam SelectClientCommand { get; set; }
        public RelayCommandWithParam SelectNoteCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand DiscardCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand EditCommand { get; set; }

        public NotesVM()
        {
            IsEditing = false;
            SelectClientCommand = new RelayCommandWithParam(SelectClient);
            SelectNoteCommand = new RelayCommandWithParam(SelectNote);
            SaveCommand = new RelayCommand(SaveChanges, CanSaveChanges);
            DiscardCommand = new RelayCommand(DiscardChanges, CanDiscardChanges);
            AddCommand = new RelayCommand(AddNote, CanAddNote);
            EditCommand = new RelayCommand(EditNote, CanEditNote);
            LoadClients();
        }

        private void EditNote()
        {
            IsEditing = true;
        }
        private bool CanEditNote()
        {
            return !IsEditing && SelectedNote != null;
        }
        private void AddNote()
        {
            Window addNote = new AddNote(new Note(), SelectedClient.ClientId);
            addNote.ShowDialog();
            LoadNotesForClient(SelectedClient);
        }
        private bool CanAddNote()
        {
            return !IsEditing && SelectedClient != null;
        }
        private void DiscardChanges()
        {
            _db.Discard();
            LoadNotesForClient(SelectedClient);
            IsEditing = false;
        }
        private bool CanDiscardChanges()
        {
            return IsEditing;
        }
        private void SaveChanges()
        {
            if (!SelectedNote.NoteName.IsNullOrEmpty())
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
        private void LoadNotesForClient(Client client)
        {
            Notes = _db.GetNotesForClient(client.ClientId);
        }
        private void SelectClient(object parameter)
        {
            if (parameter is Client client)
            {
                SelectedClient = client;
            }
        }
        private void SelectNote(object parameter)
        {
            if (parameter is Note note)
            {
                SelectedNote = note;
            }
        }
    }
}
