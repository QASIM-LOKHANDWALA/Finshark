using Finshark.Dtos.Stock;
using Finshark.Helpers;
using Finshark.Models;

namespace Finshark.Interface
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(StockQueryObject query);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(CreateStockRequestDTO stockDTO);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDTO stockDTO);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExists(int id);
    }
}
