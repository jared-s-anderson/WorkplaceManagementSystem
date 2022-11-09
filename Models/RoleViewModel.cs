using System.ComponentModel.DataAnnotations;

namespace WorkplaceManagementSystem.Models
{
    public class RoleViewModel
    {
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
