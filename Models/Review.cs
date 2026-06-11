using System.ComponentModel.DataAnnotations;

namespace SomaShare.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime DatePosted { get; set; }
        public string ReviewerId { get; set; }
        public ApplicationUser Reviewer { get; set; }
        public string RevieweeId { get; set; }
        public ApplicationUser Reviewee { get; set; }
    }
}
