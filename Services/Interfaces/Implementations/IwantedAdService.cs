using SomaShare.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SomaShare.Services
{
    public interface IWantedAdService
    {
        Task CreateAsync(WantedAd ad);
        Task UpdateAsync(WantedAd ad);
        Task DeleteAsync(int id);
        Task<List<WantedAd>> GetAllAsync();
        Task<List<WantedAd>> GetUserAdsAsync(string userId);
    }
}