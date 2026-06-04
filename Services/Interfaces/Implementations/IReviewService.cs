using System.Threading.Tasks;
using SomaShare.Models;

namespace SomaShare.Services
{
    public interface IReviewService
    {
        Task AddReviewAsync(Review review);
    }
}