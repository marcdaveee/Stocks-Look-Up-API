using StockMarket_with_Social_Media_Platform.Dtos.Stock;
using StockMarket_with_Social_Media_Platform.Models;

namespace StockMarket_with_Social_Media_Platform.Interfaces
{
    public interface IStockRepository
    {
        Task<IEnumerable<Stock>> GetStocksAsync();

        Task<Stock?> GetStockByIdAsync(int id);

        Task <Stock> CreateStockAsync(Stock stockModel);

        Task <Stock?>UpdateStockAsync (int id, UpdateStockRequestDto updatedStock);

        Task <Stock?> RemoveStockAsync(int id);

        Task<bool> isStockExistAsync(int id);
    }
}
