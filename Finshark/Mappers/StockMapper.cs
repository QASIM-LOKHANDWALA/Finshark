using Finshark.Dtos.Stock;
using Finshark.Models;

namespace Finshark.Mappers
{
    public static class StockMapper
    {
        public static StockDTO ToStockDTO(this Stock stock)
        {
            return new StockDTO
            {
                Id = stock.Id,
                CompanyName = stock.CompanyName,
                Industry = stock.Industry,
                LastDiv = stock.LastDiv,
                MarketCap = stock.MarketCap,
                Purchase = stock.Purchase,
                Symbol = stock.Symbol,
                Comments = stock.Comments.Select(c => c.ToCommentDTO()).ToList()
            };
        }

        public static Stock ToStockFromCreateDTO(this CreateStockRequestDTO stockDTO)
        {
            return new Stock
            {
                CompanyName = stockDTO.CompanyName,
                Industry = stockDTO.Industry,
                LastDiv = stockDTO.LastDiv,
                MarketCap = stockDTO.MarketCap,
                Purchase = stockDTO.Purchase,
                Symbol = stockDTO.Symbol
            };
        }
    }
}
