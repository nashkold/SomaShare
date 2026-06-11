using Microsoft.EntityFrameworkCore;
using SomaShare.Data;
using SomaShare.Models;

namespace SomaShare.Services
{
    public class FavouriteService : IFavouriteService
    {
        private readonly ApplicationDbContext _context;

        public FavouriteService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add a textbook to the user's favourites, skips if it's already favourited
        public async Task AddFavouriteAsync(string userId, int textbookId)
        {
            bool alreadyExists = await _context.Favourites
                .AnyAsync(f => f.UserId == userId && f.TextbookId == textbookId);

            if (alreadyExists) return;

            _context.Favourites.Add(new Favourite
            {
                UserId = userId,
                TextbookId = textbookId
            });

            await _context.SaveChangesAsync();
        }

        // Remove a textbook from the user's favourites, skips if no matching row is found
        public async Task RemoveFavouriteAsync(string userId, int textbookId)
        {
            var favourite = await _context.Favourites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.TextbookId == textbookId);

            if (favourite == null) return;

            _context.Favourites.Remove(favourite);
            await _context.SaveChangesAsync();
        }

        // Return all favourited textbooks for a user, newest first
        public async Task<List<Favourite>> GetUserFavouritesAsync(string userId)
        {
            return await _context.Favourites
                .Include(f => f.Textbook)
                    .ThenInclude(t => t.Seller)
                .Where(f => f.UserId == userId)
                .OrderByDescending(f => f.FavouriteId)
                .ToListAsync();
        }
    }
}