using System.Collections.Generic;
using System.Threading.Tasks;
using SomaShare.Models;

namespace SomaShare.Services
{
    public interface ITextbookService
    {
        Task<List<Textbook>> GetAllAsync();
        Task<Textbook> GetByIdAsync(int id);
        Task CreateAsync(Textbook textbook);
        Task UpdateAsync(Textbook textbook);
        Task DeleteAsync(int id, string userId);
    }
}