using Microsoft.EntityFrameworkCore;
using SomaShare.Data;
using SomaShare.Models;
using System.Collections.Generic;
using System.Linq;

namespace SomaShare.Services
{
    public class WantedAdService : IWantedAdService
    {
        private readonly ApplicationDbContext _context;

        public WantedAdService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create a new wanted ad
        public async Task CreateAsync(WantedAd ad)
        {
            _context.WantedAds.Add(ad);
            await _context.SaveChangesAsync();
        }

        // Update an existing wanted ad
        public async Task UpdateAsync(WantedAd ad)
        {
            _context.WantedAds.Update(ad);
            await _context.SaveChangesAsync();
        }

        // Delete a wanted ad 
        public async Task DeleteAsync(int id)
        {
            var ad = await _context.WantedAds.FindAsync(id);
            if (ad != null)
            {
                _context.WantedAds.Remove(ad);
                await _context.SaveChangesAsync();
            }
        }

        // Get all wanted ads with the poster's details
        public async Task<List<WantedAd>> GetAllAsync()
        {
            return await _context.WantedAds
                .Include(w => w.User)
                .OrderByDescending(w => w.DatePosted)
                .ToListAsync();
        }

        // Get only the ads posted by a specific user
        public async Task<List<WantedAd>> GetUserAdsAsync(string userId)
        {
            return await _context.WantedAds
                .Include(w => w.User)
                .Where(w => w.UserId == userId)
                .OrderByDescending(w => w.DatePosted)
                .ToListAsync();
        }
    }
}