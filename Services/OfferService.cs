using Microsoft.EntityFrameworkCore;
using SomaShare.Data;
using SomaShare.Models;

namespace SomaShare.Services
{
    public class OfferService : IOfferService
    {
        private readonly ApplicationDbContext _context;

        public OfferService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create a new offer
        public async Task MakeOfferAsync(Offer offer)
        {
            _context.Offers.Add(offer);
            await _context.SaveChangesAsync();
        }

        // Accept an offer — also creates a transaction automatically
        public async Task AcceptOfferAsync(int offerId)
        {
            var offer = await _context.Offers
                .Include(o => o.Textbook)
                .FirstOrDefaultAsync(o => o.OfferId == offerId); // fixed: OfferId not Id

            if (offer == null) return;

            // Business Rule: only one offer can be accepted per textbook
            var alreadyAccepted = await _context.Offers
                .AnyAsync(o => o.TextbookId == offer.TextbookId && o.Status == "Accepted");

            if (alreadyAccepted)
                throw new Exception("An offer has already been accepted for this textbook.");

            // Mark offer as accepted
            offer.Status = "Accepted";

            // Mark the textbook as no longer available
            offer.Textbook.IsAvailable = false;

            // Create a transaction for this accepted offer
            var transaction = new Transaction
            {
                OfferId = offer.OfferId, // fixed: OfferId not Id
                TransactionDate = DateTime.Now,
                PaymentMethod = "Cash on Meetup",
                IsComplete = false
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        // Reject an offer
        public async Task RejectOfferAsync(int offerId)
        {
            var offer = await _context.Offers.FindAsync(offerId);
            if (offer != null)
            {
                offer.Status = "Rejected";
                await _context.SaveChangesAsync();
            }
        }
    }
}