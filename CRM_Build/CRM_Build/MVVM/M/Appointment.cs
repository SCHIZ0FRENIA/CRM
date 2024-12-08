using System.ComponentModel.DataAnnotations;

namespace CRM_Build.MVVM.M
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }  // Primary key

        [Required, MaxLength(100)]
        public string Title { get; set; }  // Appointment title

        [Required]
        public DateTime Date { get; set; }  // Appointment date

        [MaxLength(500)]
        public string Description { get; set; }  // Appointment description

        [Required]
        public string Status { get; set; }  // Status: Scheduled, Completed, Canceled

        public int UserId { get; set; }  // Foreign key to User
        public User User { get; set; }
    }
}
