using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using StockMarket_with_Social_Media_Platform.Data;
using StockMarket_with_Social_Media_Platform.Dtos.Comment;
using StockMarket_with_Social_Media_Platform.Dtos.Stock;
using StockMarket_with_Social_Media_Platform.Helpers;
using StockMarket_with_Social_Media_Platform.Interfaces;
using StockMarket_with_Social_Media_Platform.Mappers;
using StockMarket_with_Social_Media_Platform.Models;

namespace StockMarket_with_Social_Media_Platform.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Stock>> GetStocksAsync(QueryObject query)
        {
            var stocks  = _context.Stocks.Include(stock => stock.Comments).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Symbol)){
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }

            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();            
        }

        public async Task<Stock?> GetStockByIdAsync(int id)
        {
            return await _context.Stocks.Include(stock => stock.Comments).FirstOrDefaultAsync(s => s.Id == id);           
        }

        public async Task <Stock> CreateStockAsync (Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();

            return stockModel;
        }

        public async Task <Stock?> UpdateStockAsync (int id, UpdateStockRequestDto updatedStock)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);

            if(stockModel == null)
            {
                return null;
            }

            stockModel.Symbol = updatedStock.Symbol;
            stockModel.CompanyName = updatedStock.CompanyName;
            stockModel.Purchase = updatedStock.Purchase;
            stockModel.LastDiv = updatedStock.LastDiv;
            stockModel.Industry = updatedStock.Industry;
            stockModel.MarketCap = updatedStock.MarketCap;

            await _context.SaveChangesAsync();

            return stockModel;
        }

        public async Task <Stock?> RemoveStockAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);

            if (stockModel == null)
            {
                return null;
            }

            _context.Stocks.Remove(stockModel);

            await _context.SaveChangesAsync();

            return stockModel;

            
        }

        public async Task<bool> isStockExistAsync(int id)
        {
            return await _context.Stocks.AnyAsync(s => s.Id == id);
        }
    }
}
