using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WorkplaceManagementSystem.Models
{
    public class EditRolesViewModel
    {
        public string Id { get; set; }

        [Required]
        [DisplayName("Role Name")]
        public string RoleName { get; set; }
        public List<string> Users { get; set; } = new();
    }
}
