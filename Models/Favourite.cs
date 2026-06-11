using System.ComponentModel.DataAnnotations;

namespace SomaShare.Models
{
    public class Favourite
    {
        [Key]
        public int FavouriteId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int TextbookId { get; set; }
        public Textbook Textbook { get; set; }
    }
}