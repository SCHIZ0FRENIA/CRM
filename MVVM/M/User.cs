using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using CRM.Core;

namespace CRM.MVVM.M
{
    [Table("users")]
    public class User : NotifyPropertyChanged
    {
        private int _userId;
        private string _username;
        private string _passwordHash;
        private string _salt;
        private string _email;
        private DateTime _createdAt;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        [StringLength(50)]
        [Column("username")]
        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        [Required]
        [Column("password_hash")]
        public string PasswordHash
        {
            get => _passwordHash;
            set
            {
                if (_passwordHash != value)
                {
                    _passwordHash = value;
                    OnPropertyChanged();
                }
            }
        }

        [Required]
        [Column("salt")]
        public string Salt
        {
            get => _salt;
            set
            {
                if (_salt != value)
                {
                    _salt = value;
                    OnPropertyChanged();
                }
            }
        }

        [Required]
        [StringLength(100)]
        [Column("email")]
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

        [Column("created_at")]
        public DateTime CreatedAt
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

        public User(int userId, string username, string passwordHash, string salt, string email, DateTime createdAt)
        {
            UserId = userId;
            Username = username;
            PasswordHash = passwordHash;
            Salt = salt;
            Email = email;
            CreatedAt = createdAt;
        }

        public User(string username, string passwordHash, string salt, string email, DateTime createdAt)
        {
            Username = username;
            PasswordHash = passwordHash;
            Salt = salt;
            Email = email;
            CreatedAt = createdAt;
        }

        public User()
        {
            CreatedAt = DateTime.Now;
        }

        public string Validate()
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                return "Username is required.";
            }

            if (Username.Length > 50)
            {
                return "Username cannot be longer than 50 characters.";
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                return "Email is required.";
            }

            if (Email.Length > 100)
            {
                return "Email cannot be longer than 100 characters.";
            }

            if (!IsValidEmail(Email))
            {
                return "Email is not in a valid format.";
            }

            return "";
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, emailPattern);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
