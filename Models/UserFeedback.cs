using System.ComponentModel.DataAnnotations;

namespace WorkplaceManagementSystem.Models
{
    public class UserFeedback
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Feedback { get; set; }
    }
}
