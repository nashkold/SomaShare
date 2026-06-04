using System.ComponentModel.DataAnnotations;

namespace SomaShare.Models
{
    public class Offer
    {
        [Key]
        public int OfferId { get; set; }
        public decimal OfferAmount { get; set; }
        public string Status { get; set; } 
        public DateTime DateMade { get; set; }

        public int TextbookId { get; set; }
        public Textbook Textbook { get; set; }

        public string BuyerId { get; set; }
        public ApplicationUser Buyer { get; set; }
    }
}
