using System.ComponentModel.DataAnnotations;

namespace TutorPro.Application.Models
{
    public class SmtpConfigurationDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string From { get; set; }
        [Required]
        public string Host { get; set; }
        [Required]
        public int Port { get; set; }
    }
}
