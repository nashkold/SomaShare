using SomaShare.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SomaShare.Services
{
    public interface ITransactionService
    {
        Task CompleteTransactionAsync(int transactionId);
        Task<List<Transaction>> GetUserTransactionsAsync(string userId);
        Task<Transaction?> GetTransactionAsync(int id);
    }
}