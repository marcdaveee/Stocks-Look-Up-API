using StockMarket_with_Social_Media_Platform.Dtos.Stock;
using StockMarket_with_Social_Media_Platform.Models;

namespace StockMarket_with_Social_Media_Platform.Mappers
{
    public static class StockMapper
    {
        public static StockDto ToStockDto(this Stock stockModel) 
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap
            };
        }

        public static Stock toStockFromCreateDto(this CreateStockRequestDto createdStock)
        {
            return new Stock
            {
                Symbol = createdStock.Symbol,
                CompanyName = createdStock.CompanyName,
                Purchase = createdStock.Purchase,
                LastDiv = createdStock.LastDiv,
                Industry = createdStock.Industry,
                MarketCap = createdStock.MarketCap
            };
        }

        public static Stock toStockFromUpdateDto(this UpdateStockRequestDto updatedStock)
        {
            return new Stock
            {
                Symbol = updatedStock.Symbol,
                CompanyName = updatedStock.CompanyName,
                Purchase = updatedStock.Purchase,
                LastDiv = updatedStock.LastDiv,
                Industry = updatedStock.Industry,
                MarketCap = updatedStock.MarketCap

            };
        }

    }
}
