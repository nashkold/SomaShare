using Microsoft.EntityFrameworkCore;
using SomaShare.Data;
using SomaShare.Models;

namespace SomaShare.Services
{

    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;

        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddReviewAsync(Review review)
        {
            // Ensure transaction completed before review
            bool valid = await _context.Transactions
                .Include(t => t.Offer)
                .AnyAsync(t =>
                    t.IsComplete &&
                    t.Offer.BuyerId == review.ReviewerId);

            if (!valid)
                throw new Exception("Cannot review without transaction");

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }
    }
}