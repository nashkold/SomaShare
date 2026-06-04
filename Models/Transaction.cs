using System.ComponentModel.DataAnnotations;

namespace SomaShare.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string PaymentMethod { get; set; } // "Cash on Meetup" etc.
        public bool IsComplete { get; set; }

        public int OfferId { get; set; }
        public Offer Offer { get; set; }
    }
}
