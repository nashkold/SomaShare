using Microsoft.EntityFrameworkCore;
using SomaShare.Data;
using SomaShare.Models;
using System.Collections.Generic;
using System.Linq;

namespace SomaShare.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;

        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Mark a transaction as complete
        public async Task CompleteTransactionAsync(int transactionId)
        {
            var transaction = await _context.Transactions.FindAsync(transactionId);
            if (transaction == null)
                throw new Exception("Transaction not found");

            transaction.IsComplete = true;
            await _context.SaveChangesAsync();
        }

        // Get all transactions where the user is either the buyer or the seller
        public async Task<List<Transaction>> GetUserTransactionsAsync(string userId)
        {
            return await _context.Transactions
                .Include(t => t.Offer)
                    .ThenInclude(o => o.Textbook)
                        .ThenInclude(t => t.Seller)
                .Include(t => t.Offer)
                    .ThenInclude(o => o.Buyer)
                .Where(t =>
                    t.Offer.BuyerId == userId ||
                    t.Offer.Textbook.SellerId == userId)
                .ToListAsync();
        }

        // Get a single transaction with full details
        public async Task<Transaction?> GetTransactionAsync(int id)
        {
            return await _context.Transactions
                .Include(t => t.Offer)
                    .ThenInclude(o => o.Textbook)
                        .ThenInclude(t => t.Seller)
                .Include(t => t.Offer)
                    .ThenInclude(o => o.Buyer)
                .FirstOrDefaultAsync(t => t.TransactionId == id);
        }
    }
}