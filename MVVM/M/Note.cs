using CRM.Core;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CRM.MVVM.M
{
    [Table("client_notes")]
    public class Note : NotifyPropertyChanged
    {
        private int _noteId;
        private int _clientId;
        private string _noteName;
        private string _note;

        [Key]
        [Column("note_id")]
        public int NoteId
        {
            get => _noteId;
            set
            {
                if (_noteId != value)
                {
                    _noteId = value;
                    OnPropertyChanged();
                }
            }
        }

        [Required]
        [Column("client_id")]
        public int ClientId
        {
            get => _clientId;
            set
            {
                if (_clientId != value)
                {
                    _clientId = value;
                    OnPropertyChanged();
                }
            }
        }

        [Required]
        [Column("note_name")]
        [StringLength(100)]
        public string NoteName
        {
            get => _noteName;
            set
            {
                if (_noteName != value)
                {
                    _noteName = value;
                    OnPropertyChanged();
                }
            }
        }

        [Required]
        [Column("note_text")]
        public string NoteText
        {
            get => _note;
            set
            {
                if (_note != value)
                {
                    _note = value;
                    OnPropertyChanged();
                }
            }
        }

        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }

        public string Validate()
        {
            if (string.IsNullOrEmpty(NoteName))
            {
                return "NoteName: is required.";
            }

            if (NoteName.Length > 100)
            {
                return "NoteName: not correct length.";
            }

            if (string.IsNullOrEmpty(NoteText))
            {
                return "Note: is required.";
            }

            return string.Empty;
        }
    }
}
