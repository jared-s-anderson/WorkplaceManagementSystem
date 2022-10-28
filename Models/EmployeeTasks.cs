using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WorkplaceManagementSystem.Models
{
    public enum IsComplete
    {
        Unfinished,
        Finished
    }

    public class EmployeeTasks
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Task Name")]
        public string TaskName { get; set; }

        [Required]
        [DisplayName("Task Description")]
        public string TaskDescription { get; set; }

        [Required]
        [DisplayName("Task Creation Date")]
        public DateTime TaskDate { get; set; } = DateTime.Now;

        [Required]
        public IsComplete Status { get; set; }

    }
}
