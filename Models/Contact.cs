using System.ComponentModel.DataAnnotations;

namespace ContactApp.Models
{
    public class Contact
    {
        [Required, Display(Name="Your Name")]
        public string Name { get; set; } = string.Empty;
        [Required, EmailAddress, Display(Name="Email Address")]
        public string Email { get; set; } = string.Empty;
        [Required, StringLength(300, ErrorMessage = "Message must be 300 characters or fewer.")]
        public string Message { get; set; } = string.Empty;
        public DateTime SubmittedUtc { get; set; } = DateTime.UtcNow;
    }
}
