using System.ComponentModel.DataAnnotations;

namespace SomaShare.Models
{
    public class Advert
    {
        [Key]
        public int AdvertId { get; set; }
        public string Title { get; set; }
        public string CourseCode { get; set; }
        public decimal MaxPrice { get; set; }
        public DateTime DatePosted { get; set; }

        public string PostedById { get; set; }
        public ApplicationUser PostedBy { get; set; }
    }
}
