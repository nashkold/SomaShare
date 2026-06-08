using System.Threading.Tasks;
using SomaShare.Models;

namespace SomaShare.Services
{
    public interface IReviewService
    {
        Task AddReviewAsync(Review review);

        Task<List<Review>> GetUserReviewsAsync(string userId);

        Task<double> CalculateTrustScoreAsync(string userId);
    }
}