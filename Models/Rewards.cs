using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkplaceManagementSystem.Models
{
    public class Rewards
    {
        [Key]
        public int RewardId { get; set; }

        [Required]
        public string Reward { get; set; }

        [Required]
        public string RewardDescription { get; set; }

    }
}
