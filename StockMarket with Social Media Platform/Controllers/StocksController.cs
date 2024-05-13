using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockMarket_with_Social_Media_Platform.Data;
using StockMarket_with_Social_Media_Platform.Dtos.Stock;
using StockMarket_with_Social_Media_Platform.Mappers;
using StockMarket_with_Social_Media_Platform.Models;

namespace StockMarket_with_Social_Media_Platform.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult <IEnumerable<StockDto>>> GetStocks()
        {
            var stocks = await _context.Stocks.ToListAsync();

            var stockDto = stocks.Select(s => s.ToStockDto());

            return Ok(stockDto);
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> GetStockById([FromRoute] int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stock == null)
            {   
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]

        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDto createdStock)
        {
            var stockModel = createdStock.toStockFromCreateDto();

            await _context.Stocks.AddAsync(stockModel);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStockById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut("{id}")]
        public async Task <IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDto updatedStock)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);

            if(stockModel == null)
            {
                return NotFound();
            }

            stockModel.Symbol = updatedStock.Symbol;
            stockModel.CompanyName = updatedStock.CompanyName;
            stockModel.Purchase = updatedStock.Purchase;
            stockModel.LastDiv = updatedStock.LastDiv;
            stockModel.Industry = updatedStock.Industry;
            stockModel.MarketCap = updatedStock.MarketCap;

            await _context.SaveChangesAsync();

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{stockId}")]

        public async Task<IActionResult> DeleteStock([FromRoute] int stockId)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == stockId);

            if(stockModel == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stockModel);

            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
