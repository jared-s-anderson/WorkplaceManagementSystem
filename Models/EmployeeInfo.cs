using System.ComponentModel.DataAnnotations;

namespace WorkplaceManagementSystem.Models
{
    public class EmployeeInfo
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int YearsAtWork { get; set; }

        [Required]
        public int TasksCompleted { get; set; }
    }
}
