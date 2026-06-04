using System.Threading.Tasks;

namespace SomaShare.Services
{
    public interface ITransactionService
    {
        Task CompleteTransactionAsync(int transactionId);
    }
}