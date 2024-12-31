using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CRM.Core;
using System.Text.RegularExpressions;

namespace CRM.MVVM.M
{
    [Table("clients")]
    public class Client : NotifyPropertyChanged
    {
        private int _clientId;
        private int? _userId;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phoneNumber;
        private string _companyName;
        private string _jobTitle;
        private DateTime? _createdAt;
        private DateTime? _updatedAt;

        [Key]
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

        [Column("user_id")]
        public int? UserId
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
        [Column("first_name")]
        [StringLength(50)]
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged();
                }
            }
        }

        [Required]
        [Column("last_name")]
        [StringLength(50)]
        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        [Required]
        [Column("email")]
        [StringLength(100)]
        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }

        [Column("phone_number")]
        [StringLength(20)]
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        [Column("company_name")]
        [StringLength(100)]
        public string CompanyName
        {
            get => _companyName;
            set
            {
                if (_companyName != value)
                {
                    _companyName = value;
                    OnPropertyChanged();
                }
            }
        }

        [Column("job_title")]
        [StringLength(50)]
        public string JobTitle
        {
            get => _jobTitle;
            set
            {
                if (_jobTitle != value)
                {
                    _jobTitle = value;
                    OnPropertyChanged();
                }
            }
        }

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

        [Column("updated_at")]
        public DateTime? UpdatedAt
        {
            get => _updatedAt;
            set
            {
                if (_updatedAt != value)
                {
                    _updatedAt = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Name { get => FirstName + ' ' + LastName + " - " + CompanyName; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public string Validate()
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                return "FirstName: is required.";
            }
            
            if (FirstName.Length > 50)
            {
                return "FirstName: not correct length.";
            }

            if (string.IsNullOrEmpty(LastName))
            {
                return "LastName: is required.";
            }
            else if (LastName.Length > 50)
            {
                return "LastName: not correct length.";
            }

            if (string.IsNullOrEmpty(Email))
            {
                return "Email: is required.";
            }
            
            if (Email.Length > 100)
            {
                return "Email: not correct length.";
            }

            if (string.IsNullOrEmpty(PhoneNumber) || PhoneNumber.Length > 20)
            {
                return "PhoneNumber: not correct length.";
            }

            if (string.IsNullOrEmpty(CompanyName) || CompanyName.Length > 100)
            {
                return "CompanyName: not correct length.";
            }

            if (string.IsNullOrEmpty(JobTitle) || JobTitle.Length > 50)
            {
                return "JobTitle: not correct length.";
            }

            return string.Empty;
        }
    }
}
