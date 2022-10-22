using System.ComponentModel.DataAnnotations;

namespace WorkplaceManagementSystem.Models
{
    public class EmployeeTasks
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TaskName { get; set; }

        [Required]
        public string TaskDescription { get; set; }

        [Required]
        public DateTime TaskDate { get; set; } = DateTime.Now;

        [Required]
        public bool IsCompleted { get; set; }

        public EmployeeTasks()
        {
            IsCompleted = false;
        }


    }
}
