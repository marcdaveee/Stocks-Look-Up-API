using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockMarket_with_Social_Media_Platform.Data;
using StockMarket_with_Social_Media_Platform.Dtos.Stock;
using StockMarket_with_Social_Media_Platform.Interfaces;
using StockMarket_with_Social_Media_Platform.Mappers;
using StockMarket_with_Social_Media_Platform.Models;

namespace StockMarket_with_Social_Media_Platform.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockRepository _stockRepo;

        public StocksController(ApplicationDbContext context, IStockRepository stockRepo)
        {
            _context = context;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<ActionResult <IEnumerable<StockDto>>> GetStocks()
        {
            var stocks = await _stockRepo.GetStocksAsync();
            var stockDtos = stocks.Select(s => s.ToStockDto());
            return Ok(stockDtos);
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> GetStockById([FromRoute] int id)
        {
            var stock = await _stockRepo.GetStockByIdAsync(id);
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
             await _stockRepo.CreateStockAsync(stockModel);

            return CreatedAtAction(nameof(GetStockById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut("{id}")]
        public async Task <IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDto updatedStock)
        {
            var stockModel = await _stockRepo.UpdateStockAsync(id, updatedStock);

            if(stockModel == null)
            {
                return NotFound();
            }            

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{stockId}")]

        public async Task<IActionResult> DeleteStock([FromRoute] int stockId)
        {
            var stockModel = await _stockRepo.RemoveStockAsync(stockId);

            if(stockModel == null)
            {
                return NotFound();
            }

            return NoContent();

        }
    }
}
