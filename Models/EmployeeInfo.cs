using System.ComponentModel.DataAnnotations;

namespace WorkplaceManagementSystem.Models
{
    public class EmployeeInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public int PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Work Years")]
        public int YearsAtWork { get; set; }

        [Required]
        [Display(Name = "Completed Tasks")]
        public int TasksCompleted { get; set; }
    }
}
