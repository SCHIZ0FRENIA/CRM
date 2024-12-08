using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Build.MVVM.M
{
    public class User
    {
        [Key]
        public int Id { get; set; }  // Primary key

        [Required, MaxLength(50)]
        public string Username { get; set; }  // Unique username

        [Required]
        public string PasswordHash { get; set; }  // Password hash

        [Required]
        public string Role { get; set; }  // Role: Manager, Customer, Admin

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Surname { get; set; }

        [MaxLength(100)]
        public string SecondName { get; set; }

        [MaxLength(150)]
        public string Organization { get; set; }
    }
}
