using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CRM.Core;

namespace CRM.MVVM.M
{
    [Table("appointments")]
    public class Appointment : NotifyPropertyChanged
    {
        private int _appointmentId;
        private int _userId;
        private int _clientId;
        private DateTime _appointmentDate;
        private string _subject;
        private string _notes;
        private DateTime? _createdAt;

        [Key]
        [Column("appointment_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentId
        {
            get => _appointmentId;
            set
            {
                if (_appointmentId != value)
                {
                    _appointmentId = value;
                    OnPropertyChanged();
                }
            }
        }

        [Required]
        [Column("user_id")]
        public int UserId
        {
            get => _userId;
            set
            {
                if (_userId != value)
                {
                    _userId = value;
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
        [Column("appointment_date")]
        public DateTime AppointmentDate
        {
            get => _appointmentDate;
            set
            {
                if (_appointmentDate != value)
                {
                    _appointmentDate = value;
                    OnPropertyChanged();
                }
            }
        }

        [Required]
        [Column("subject")]
        [StringLength(100)]
        public string Subject
        {
            get => _subject;
            set
            {
                if (_subject != value)
                {
                    _subject = value;
                    OnPropertyChanged();
                }
            }
        }

        [DataType(DataType.DateTime)]
        [Column("created_at")]
        public DateTime? CreatedAt
        {
            get => _createdAt;
            set
            {
                if (_createdAt != value)
                {
                    _createdAt = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Validate()
        {
            if (string.IsNullOrEmpty(Subject))
            {
                return "Subject: is required.";
            }

            if (Subject.Length > 100)
            {
                return "Subject: not correct length.";
            }

            return string.Empty;
        }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }
    }
}
