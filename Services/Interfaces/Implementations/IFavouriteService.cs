using SomaShare.Models;

namespace SomaShare.Services
{
    public interface IFavouriteService
    {
        Task AddFavouriteAsync(string userId, int textbookId);
        Task RemoveFavouriteAsync(string userId, int textbookId);
        Task<List<Favourite>> GetUserFavouritesAsync(string userId);
    }
}