using Microsoft.AspNetCore.Identity;

namespace SomaShare.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? Campus { get; set; }
        public string? Role { get; set; }
        public int TrustScore { get; set; }
        public DateTime DateJoined { get; set; }

        // Reviews only — Textbooks and Offers are handled via SellerId/BuyerId
        public ICollection<Review> ReviewsGiven { get; set; }
        public ICollection<Review> ReviewsReceived { get; set; }
    }
}