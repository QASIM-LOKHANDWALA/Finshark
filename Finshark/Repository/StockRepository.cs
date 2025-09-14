using Finshark.Data;
using Finshark.Dtos.Stock;
using Finshark.Interface;
using Finshark.Mappers;
using Finshark.Models;
using Microsoft.EntityFrameworkCore;

namespace Finshark.Repository
{
    public class StockRepository(ApplicationDBContext context) : IStockRepository
    {
        private readonly ApplicationDBContext _context = context;

        public async Task<Stock> CreateAsync(CreateStockRequestDTO stockDTO)
        {
            var stock = stockDTO.ToStockFromCreateDTO();
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stock is null)
            {
                return null;
            }
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            var stocks = await _context.Stocks.Include(s => s.Comments).ToListAsync();
            return stocks;
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            var stock = await _context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(s => s.Id == id);
            if (stock is null)
            {
                return null;
            }
            return stock;
        }

        public async Task<bool> StockExists(int id)
        {
            var stockExists = await _context.Stocks.AnyAsync(s => s.Id == id);
            return stockExists;
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDTO stockDTO)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stock is null)
            {
                return null;
            }
            stock.Symbol = stockDTO.Symbol;
            stock.MarketCap = stockDTO.MarketCap;
            stock.Purchase = stockDTO.Purchase;
            stock.LastDiv = stockDTO.LastDiv;
            stock.CompanyName = stockDTO.CompanyName;
            stock.Industry = stockDTO.Industry;

            await _context.SaveChangesAsync();
            return stock;
        }
    }
}
