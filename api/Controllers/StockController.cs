using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Modles;
using api.Mappers;
using api.Dtos.Stock;
using System.Linq;
using System.Threading.Tasks;
using api.Repository;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly StockRepository _stockRepository;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
            _stockRepository = new StockRepository(context);
        }

        [HttpGet]
        public async Task<IActionResult> GetStocks()
        {
            var stocks = await _stockRepository.GetAllStocksAsync();
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStock(int id)
        {
            var stock = await _stockRepository.GetStockByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.TostockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {  
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockModel = stockDto.ToStockFromCreateStockRequestDto();

            if (stockModel == null)
            {
                return BadRequest("Invalid stock data.");
            }

            var stock = await _stockRepository.CreateStockAsync(stockModel);

            return CreatedAtAction(nameof(GetStock), new { id = stockModel.Id }, stockModel.TostockDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var stock = await _stockRepository.DeleteStockAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("{id}")]   
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockModel = await _stockRepository.UpdateStockAsync(id, stockDto);

            if (stockModel == null)
            {
                return NotFound();
            }
            
            return NoContent();
        }
    }
} 
