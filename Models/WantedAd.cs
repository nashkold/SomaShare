using System.ComponentModel.DataAnnotations;
using SomaShare.Models;

namespace SomaShare.Models
{
    public class WantedAd
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal MaxPrice { get; set; }

        public DateTime DatePosted { get; set; } = DateTime.Now;

        // Foreign Key — who posted this wanted ad
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
