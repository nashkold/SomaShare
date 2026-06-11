using Microsoft.EntityFrameworkCore;
using SomaShare.Data;
using SomaShare.Models;

namespace SomaShare.Services
{
    public class TextbookService : ITextbookService
    {
        private readonly ApplicationDbContext _context;

        public TextbookService(ApplicationDbContext context)
        {
            _context = context;
        }

        // List Textbook
        public async Task<List<Textbook>> GetAllAsync()
        {
            return await _context.Textbooks
            .Include(t => t.Seller)
            .ToListAsync();
        }

            public async Task<Textbook> GetByIdAsync(int id)
        {
            return await _context.Textbooks
                .Include(t => t.Offers)
                .FirstOrDefaultAsync(t => t.TextbookId == id);
        }

        // Create Textbook
        public async Task CreateAsync(Textbook textbook)
        {
            _context.Textbooks.Add(textbook);
            await _context.SaveChangesAsync();
        }

        // Update Textbook
        public async Task UpdateAsync(Textbook textbook)
        {
            _context.Textbooks.Update(textbook);
            await _context.SaveChangesAsync();
        }

        // Delete Textbook (only if user is the seller)
        public async Task DeleteAsync(int id, string userId)
        {
            var textbook = await _context.Textbooks.FindAsync(id);

            if (textbook == null)
                throw new Exception("Textbook not found");

            if (textbook.SellerId != userId)
                throw new UnauthorizedAccessException();

            _context.Textbooks.Remove(textbook);
            await _context.SaveChangesAsync();
        }
    }
}