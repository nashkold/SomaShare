using System.Threading.Tasks;
using SomaShare.Models;

namespace SomaShare.Services
{ 
    public interface IOfferService
    {
        Task MakeOfferAsync(Offer offer);
        Task AcceptOfferAsync(int offerId);
        Task RejectOfferAsync(int offerId);
        Task<List<Offer>> GetOffersForSellerAsync(string sellerId);
        Task<List<Offer>> GetOffersByBuyerAsync(string buyerId);
    }
}
