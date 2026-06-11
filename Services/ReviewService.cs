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
            // Ensures the transaction is completed before a review can be submitted
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

        public async Task<List<Review>> GetUserReviewsAsync(string userId)
        {
            return await _context.Reviews
                .Where(r => r.RevieweeId == userId)
                .OrderByDescending(r => r.DatePosted)
                .ToListAsync();
        }

        public async Task<double> CalculateTrustScoreAsync(string userId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.RevieweeId == userId)
                .ToListAsync();

            if (!reviews.Any())
                return 0;

            return reviews.Average(r => r.Rating);
        }
    }
}